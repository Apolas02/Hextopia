using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.UI;

public class UICanvasScript : MonoBehaviour
{

    public clickTarget clickTargetScript;
    //public GameObject civilian;
    public GameObject target;
    Animator anim;
    

    public string[] houseObjects = { };
    
    public int storehouseCount = 0;
    public int townCenterCount = 0;
    public string[] civObjects = { }; //holds NPC objects to be spawned
    public GameObject[] storehouses = { };
    public GameObject[] houselvl1Count = { };
    public NavMeshAgent npcAgent;
    public Vector3 npcAgentDestination;

    CivilianScript targetCivilianScript;
    ResourceCounter resourceCounter;

    // UI buttons
    GameObject recycleButton;
    GameObject townCenterButton;
    GameObject civilianButton;
    GameObject houseButton;
    GameObject moveOrderButton;
    Dropdown jobSelectorDropdown;
    GameObject storehouseButton;

    public int optionIndex; // jobdropdown index
    public int houseCount;
    public int popLimit;
    int wood = 20;
    int stone = 20;

    public List<GameObject> builtObjectList = new List<GameObject>();

    


    // Start is called before the first frame update
    void Start()
    {
        resourceCounter = gameObject.GetComponent<ResourceCounter>();
        startState();
    }

    // Update is called once per frame
    void Update()
    {
        ResourceUICount();
        countBuidlings();
        populationLimit();
        CalculateResCount();
        checkRequiredBuildings();
        checkBuildingRequiredResourses();
        showMenuItems();
        target = clickTargetScript.clickedTarget;
    }

    void startState()
    {
        
        recycleButton = transform.Find("BuildPanel/Recycle").gameObject;
        townCenterButton = transform.Find("BuildPanel/TownCenter").gameObject;
        civilianButton = transform.Find("BuildPanel/Civilian").gameObject;
        houseButton = transform.Find("BuildPanel/House").gameObject;
        moveOrderButton = transform.Find("BuildPanel/MoveOrder").gameObject;
        storehouseButton = transform.Find("BuildPanel/Storehouse").gameObject;
        jobSelectorDropdown = transform.Find("BuildPanel/JobDropdown").GetComponent<Dropdown>();
        




        townCenterButton.SetActive(true);
        houseButton.SetActive(false);
        storehouseButton.SetActive(false);
        removeTCMenuItems();
        removeHouseMenuItems();
        removeCivMenuItems();
        removeRecycle();
    }

    // called by clicking on UI button, recycles selected target
    public void recycleObject()
    {
        Destroy(target);
        clickTargetScript.clickedTarget = null;
        if (checkCivObjects() == true) resourceCounter.SetPopulation(resourceCounter.GetPopulation() - 1);
        storehouseCount -= 1;
    }

    void showRecycle()
    {
        recycleButton.SetActive(true);
    }
    void removeRecycle()
    {
        recycleButton.SetActive(false);
    }

    void showMenuItems()
    {
        if (townCenterCount != 0)
        {
            showHouseMenuItems();
            removeTCMenuItems();
        }
        else
        {
            showTCMenuItems();
            removeHouseMenuItems();
        }

        if (target != null)
        {
            showRecycle();
            if (checkHouseObjects() == true)
            {
                showHouseMenuItems();
            }
            else
            {
                removeHouseMenuItems();
            }

            if (target.tag == "towncenter_lvl1")
            {
                
                showTCMenuItems();
            }
            else
            {
                removeTCMenuItems();
            }

            if (checkCivObjects() == true)
            {
                showCivMenuItems();
            }
            else
            {
                removeCivMenuItems();
            }
        }
        else
        {
            removeRecycle();
            removeHouseMenuItems();
            removeTCMenuItems();
            removeCivMenuItems();
        }
    }


    ///// Houses on click ///////
    // checks a list of valid objects
    bool checkHouseObjects()
    {
        for (int i = 0; i < houseObjects.Length; i++)
        {
            if (target.gameObject.name.Equals(houseObjects[i]))
            {
                return true;
            }
        }
        return false;
    }

    // displays menu items for house objects
    void showHouseMenuItems()
    {

    }

    // remove menu items for house objects
    void removeHouseMenuItems()
    {

    }

    ///// Town Center on click /////

    // displays menu items for tc objects
    void showTCMenuItems()
    {
        if (houseCount == 0) civilianButton.SetActive(false);
        else civilianButton.SetActive(true);
    }

    // remove menu items for tc objects
    void removeTCMenuItems()
    {
        civilianButton.SetActive(false);
    }


    ///// NPCs on click ///////
    bool checkCivObjects()
    {
        for (int i = 0; i < civObjects.Length; i++)
        {
            if (target.gameObject.name.Equals(civObjects[i]))
            {
                return true;
            }
        }
        return false;
    }

    void showCivMenuItems()
    {

        moveOrderButton.SetActive(true);
        targetCivilianScript = target.GetComponent<CivilianScript>();
        jobSelectorDropdown.gameObject.SetActive(true);
        switch (targetCivilianScript.getJob())
        {
            case "Unemployed":
                jobSelectorDropdown.value = 0;
                break;
            case "Logger":
                jobSelectorDropdown.value = 1;
                break;
            case "Builder":
                jobSelectorDropdown.value = 2;
                break;
        }


        jobSelectorDropdown.enabled = true;

    }

    void removeCivMenuItems()
    {
        moveOrderButton.SetActive(false);
        //jobSelectorDropdown.SetActive(false);
        jobSelectorDropdown.gameObject.SetActive(false);
    }

    ///// Check requirement buildings /////

    void checkRequiredBuildings()
    {
        if (townCenterCount == 0)
        {
            townCenterButton.SetActive(true);
            storehouseButton.SetActive(false);
        }
        else
        {
            townCenterButton.SetActive(false);
            storehouseButton.SetActive(true);
        }

        if (storehouseCount == 0) houseButton.SetActive(false);
        else houseButton.SetActive(true);
    }

    void checkBuildingRequiredResourses()
    {
        //if (wood < 25) houseButton.GetComponent<Button>().enabled = false;
        //else houseButton.SetActive(true);


    }

    public void countBuidlings()
    {
        houselvl1Count = GameObject.FindGameObjectsWithTag("house_lvl1");
        townCenterCount = GameObject.FindGameObjectsWithTag("towncenter_lvl1").Length;
        storehouseCount = GameObject.FindGameObjectsWithTag("storehouse_lvl1").Length;
        houseCount = houselvl1Count.Length;
    }

    public void jobSelector()
    {
        optionIndex = jobSelectorDropdown.value;
        targetCivilianScript.setJob(jobSelectorDropdown.options[optionIndex].text);
        targetCivilianScript.setWaitTime(0f);
    }


    void CalculateResCount()
    {
        storehouseScript SHscript;
        if (storehouseCount > 0)
        {
            foreach (GameObject storehouse in GameObject.FindGameObjectsWithTag("storehouse_lvl1"))
            {
                SHscript = storehouse.GetComponent<storehouseScript>();
                wood = SHscript.GetWood();
                stone = SHscript.GetStone();
            }
        }
    }

    void populationLimit()
    {
        
        popLimit = houselvl1Count.Length * 2;
        resourceCounter.SetPopulationLimit(popLimit);
    }

    void ResourceUICount()
    {
        Text woodCountUI = transform.Find("ResourceCounts/Wood/WoodCount").GetComponent<Text>();
        Text stoneCountUI = transform.Find("ResourceCounts/Stone/StoneCount").GetComponent<Text>();
        Text popCountUI = transform.Find("ResourceCounts/Population/PopulationCount").GetComponent<Text>();
        Text maxPopUI = transform.Find("ResourceCounts/MaxPop/MaxPopCount").GetComponent<Text>();

        woodCountUI.text = wood.ToString();
        stoneCountUI.text = stone.ToString();
        popCountUI.text = resourceCounter.GetPopulation().ToString();
        maxPopUI.text = resourceCounter.GetPopulationLimit().ToString();
    }
}