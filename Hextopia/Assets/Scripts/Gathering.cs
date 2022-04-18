using System;
using System.Collections;
using UnityEngine;

public class Gathering : MonoBehaviour
{
    private Vector3 targetObjectPosition;
    public GameObject TreeResource;
    public GameObject Stockpile;
    public GameObject Ground;
    private double  minGatherDis = 1;
    private double targetDisX = 0.0;
    private double targetDisZ = 0.0;
    public float maxCarryRes = 10;
    public float currentTreeRes = 0;
    public GameObject targetObject = null;
    private float moveSpeed = 1;
    public bool _treeRes = false;
    public bool _stockpile = false;
    public float startTime = 0.0f;
    public bool timerEnded = true;


    //private float gameTimeStore = 5;
    //private float gatherTime = 5; 

    //void gatherSpeed()
    //{
    //    gatherTime = gatherTimeStore;
    //    if (gatherTime > 0)
    //    {
    //        gatherTime -= Time.deltaTime;
    //    }
    //}
    void selectTarget()
    {
        if (currentTreeRes == maxCarryRes)
        {
            targetObject = Stockpile;
        }
        if (currentTreeRes == 0)
        {
            targetObject = TreeResource;
        }

    }
    

    void locateTargetPosition()
    {
        targetObjectPosition = targetObject.transform.position;
        targetDisX = Math.Abs(transform.position.x - targetObjectPosition.x);
        targetDisZ = Math.Abs(transform.position.z - targetObjectPosition.z);
    }
    

    public bool moveToTarget()
    {
        if (targetDisX > minGatherDis || targetDisZ > minGatherDis)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                                                     new Vector3(targetObjectPosition.x, Ground.transform.position.y, targetObjectPosition.z),
                                                     Time.deltaTime * moveSpeed);
            return true;
        }
        else
        {
            return false;
        }
    }

    void atLoaction()
    {
        if (moveToTarget() == false && targetObject == TreeResource)
        {
            _treeRes = true;
        }
        else
        {
            _treeRes = false;
        }

        if (moveToTarget() == false && targetObject == Stockpile)
        {
            _stockpile = true;
        }
        else
        {
            _stockpile = false;
        }
    }

    void gatherResource()
    {
        locateTargetPosition();
        if (targetObject == TreeResource & moveToTarget() == false & currentTreeRes <= maxCarryRes)
        {
            for (int i = 0; i < maxCarryRes; i++)
            {
                if (timerEnded == true)
                {
                    startTime += 1;
                    currentTreeRes += 1;
                    //TreeResource.GetComponent<resourceScript>().treeResRemaining -= 1;
                    timerEnded = false;
                   
                }
            }
        }

        if (targetObject == Stockpile & moveToTarget() == false & currentTreeRes > 0)
        {
            for (int i = 0; i < maxCarryRes; i++)
            {
                if (timerEnded == true)
                {
                    startTime += 1;
                    currentTreeRes -= 1;
                    Stockpile.GetComponent<Stockpile>().wood += 1;
                    timerEnded = false;
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        startTime -= Time.deltaTime;
        if(startTime <= 0.0f)
        {
            startTime = 0;
            timerEnded = true;
        }
        else
        {
            timerEnded = false;
        }

        selectTarget();
        atLoaction();
        gatherResource();
        
    }
}

