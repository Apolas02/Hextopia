using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class clickTarget : MonoBehaviour
{
    RaycastHit hit;
    Vector3 movePoint;
    public GameObject clickedTarget;
    public string[] validObjects = { };
    

    // Start is called before the first frame update
    void Start()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        clickedTarget = null;

        if (Physics.Raycast(ray, out hit, 50000.0f, (0 << 8)))
        {
            transform.position = hit.point;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (clickUI() == false)
        {
            clickItem();
        }
        
    }

    // check if hovering over UI
    bool clickUI()
    {
        if (EventSystem.current.currentSelectedGameObject != false) 
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // selects and outlines target, also removes previously selected target and outline
    void clickItem()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // if right mouse button 
        if (Input.GetMouseButtonDown(0))
        {
            // if clicked on valid
            if (Physics.Raycast(ray, out hit, 50.0f) && checkValidObject() == true)
            {
                // if current target is null, set and outline target
                if (clickedTarget == null)
                {
                    clickedTarget = hit.collider.gameObject;
                    outlineItem();
                }
                // if current target is not null, destroy previous target outline and set target
                else
                {
                    destroyOutline();
                    clickedTarget = hit.collider.gameObject;
                    outlineItem();
                }
            }
            // if clicked on invalid object
            else
            {
                // if previous target was valid
                if (clickedTarget != null)
                {
                    destroyOutline();
                    clickedTarget = null;
                }
            }
        }
    }

    // outlines clickedTarget
    void outlineItem()
    {
        if (clickedTarget.GetComponents<Outline>().Length == 0)
        {
            clickedTarget.AddComponent<Outline>();
        }
    }

    // destroys clickedTarget outline
    void destroyOutline()
    {
        Destroy(clickedTarget.GetComponent<Outline>());
    }

    bool checkValidObject()
    {
        for (int i = 0; i < validObjects.Length; i++)
        {
            if (hit.collider.gameObject.name.Equals(validObjects[i]))
            {
                return true;
            }
        }
        return false;
    }
}
