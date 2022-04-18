using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerColliderScript : MonoBehaviour
{
    public bool spawnerBlocked;
    public townCenterScript townCenter;
    string spawnername;
    string stringIndex;
    int index;
    // Start is called before the first frame update
    void Start()
    {
        spawnername = name;
        stringIndex = name.TrimEnd();
        index = int.Parse(stringIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnerBlocked == true)
        {
            //townCenter.spawners.SetValue(spawnername, index);
        }
        else
        {
            //townCenter.spawners.SetValue(null , index);
        } 
    }

    void OnTriggerEnter(Collider other)
    {
        spawnerBlocked = true;
    }

    void OnTriggerStay(Collider other)
    {
        spawnerBlocked = true;
    }

    void OnTriggerExit(Collider other)
    {
        spawnerBlocked = false;
    }
}
