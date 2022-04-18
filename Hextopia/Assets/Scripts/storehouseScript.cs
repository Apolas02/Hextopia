using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storehouseScript : MonoBehaviour
{
    public int wood;
    public float load;
    public float maxLoad = 100;
    public ResourceCounter ResourceCounter;



    public Dictionary<string, int> stored = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CalculateTotalLoad();
        ShowLoad();
    }

    void CalculateTotalLoad()
    {
        load = wood;
    }

    void ShowLoad()
    {
        float lowLoad = maxLoad / 10f;
        float medLoad = maxLoad / 2f;
        float highLoad = maxLoad / 1.25f;
        if (load >= highLoad)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(true);
        }

        else if (load >= medLoad)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(false);
        }

        else if (load >= lowLoad)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
        }

        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    public void AddWood(int w)
    {
        wood += w;
    }

    public int GetWood()
    {
        return wood;
    }

}
