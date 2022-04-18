using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resourceScript : MonoBehaviour
{
    //Private core values.
    public string material;
    public int load;
    public int maxLoad;

    public UICanvasScript UIScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OutOfRes();
    }

    public void OutOfRes()
    {
        if (load <= 0)
        {
            UIScript.treeList.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    public void setResourceType(string rt)
    {
        material = rt;

    }
    public void setLoad(int l)
    {
        load = l;
    }
    public void setMaxLoad(int ml)
    {
        maxLoad = ml;
    }

    public int getLoad()
    {
        return load;
    }

    public int getMaxLoad()
    {
        return maxLoad;
    }


    //CONSTRUCTORS
    public resourceScript()
    {
        material = "wood";
        load = 10;
        maxLoad = 10;

    }
    public resourceScript(string m, int l, int ml)
    {
        material = m;
        load = l;
        maxLoad = ml;
    }
}
