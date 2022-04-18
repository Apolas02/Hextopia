using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class townCenterScript : MonoBehaviour
{
    public bool spawner1, spawner2, spawner3, spawner4;
    public UICanvasScript ui;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (spawner1 == true)
        {
            Debug.Log("spawner 1 blocked");
        }
        if (spawner2 == true)
        {
            Debug.Log("spawner 2 blocked");
        }
        if (spawner3 == true)
        {
            Debug.Log("spawner 3 blocked");
        }
        if (spawner4 == true)
        {
            Debug.Log("spawner 4 blocked");
        }
    }
    

}


