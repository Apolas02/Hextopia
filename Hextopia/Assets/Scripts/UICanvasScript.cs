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
    public string[] TCObjects = { };
    public GameObject[] storehouseCount = { };
    public string[] civObjects = { };
    public GameObject[] tcCount = { };
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

    public int houseCount;
    public int civCount;
    public int popLimit;
    int woodCount;
    public List<GameObject> treeList = new List<GameObject>();
    public List<GameObject> builtObjectList = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        resourceCounter = gameObject.GetComponent<ResourceCounter>();
        startState();
        PopulateResourceLists();
    }

    // Update is called once per frame
    void Update()
    {
        ResourceCount();
        countBuidlings();
        populationLimit();
        checkRequiredBuildings();
        CalculateWoodCount();
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
        if (checkCivObjects() == true)
        {
            civCount -= 1;
        }
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
        if (tcCount.Length != 0)
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

            if (checkTCObjects() == true)
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
    // checks a list of valid objects
    bool checkTCObjects()
    {
        for (int i = 0; i < TCObjects.Length; i++)
        {
            if (target.gameObject.name.Equals(TCObjects[i]))
            {
                return true;
            }
        }
        return false;
    }

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
        if (tcCount.Length == 0)
        {
            townCenterButton.SetActive(true);
            storehouseButton.SetActive(false);
        }
        else
        {
            townCenterButton.SetActive(false);
            storehouseButton.SetActive(true);
        }

        if (storehouseCount.Length == 0) houseButton.SetActive(false);
        else houseButton.SetActive(true);
    }

    public void countBuidlings()
    {
        tcCount = GameObject.FindGameObjectsWithTag("towncenter_lvl1");
        storehouseCount = GameObject.FindGameObjectsWithTag("storehouse_lvl1");
        houselvl1Count = GameObject.FindGameObjectsWithTag("house_lvl1");
        houseCount = houselvl1Count.Length;
    }

    void PopulateResourceLists()
    {
        foreach (GameObject tree_1 in GameObject.FindGameObjectsWithTag("tree"))
        {
            treeList.Add(tree_1);
        }
    }

    public void jobSelector()
    {
        int optionIndex = jobSelectorDropdown.value;
        target.GetComponent<CivilianScript>().setJob(jobSelectorDropdown.options[optionIndex].text);
    }


    int CalculateWoodCount()
    {
        int wood = 20;
        if (storehouseCount.Length > 0)
        {
            foreach (GameObject storehouse in storehouseCount)
            {
                storehouseScript SHscript = storehouse.GetComponent<storehouseScript>();
                wood = SHscript.GetWood();
            }
        }
        return wood;
    }

    void populationLimit()
    {
        
        popLimit = houselvl1Count.Length * 2;
        resourceCounter.SetPopulationLimit(popLimit);
    }

    void ResourceCount()
    {
        Text woodCountUI = transform.Find("ResourceCounts/Wood/WoodCount").GetComponent<Text>();
        Text popCountUI = transform.Find("ResourceCounts/Population/PopulationCount").GetComponent<Text>();
        Text maxPopUI = transform.Find("ResourceCounts/MaxPop/MaxPopCount").GetComponent<Text>();

        woodCountUI.text = CalculateWoodCount().ToString();
        popCountUI.text = resourceCounter.GetPopulation().ToString();
        maxPopUI.text = resourceCounter.GetPopulationLimit().ToString();
    }
}