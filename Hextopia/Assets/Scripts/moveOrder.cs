using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class moveOrder : MonoBehaviour
{
    NavMeshAgent npcAgent;
    public clickTarget clickTargetScript;
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = clickTargetScript.clickedTarget;
        Debug.Log("Move Order");
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        npcAgent = target.GetComponent<NavMeshAgent>();

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left Click");
            if (Physics.Raycast(ray, out hitInfo, 100))
            {
                Debug.Log(hitInfo);
                npcAgent.SetDestination(hitInfo.point);
                Destroy(gameObject);
            }
        }
    }
}
