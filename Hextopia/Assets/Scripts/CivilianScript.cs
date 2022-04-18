﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CivilianScript : MonoBehaviour
{
    //Private members. This classes core values.
    public int health;
    public GameObject target;
    public string job;
    public float gatherSpeed;
    public float gatherTimer = 2;
    public float unloadTimer = 5;
    public int load;
    public int maxLoad;
    public float moveSpeed = .1f;
    public string loadMaterial;
    public float distanceToTarget;


    //Job stuff
    public UICanvasScript UIScript;
    public resourceScript targetResScript;
    public GameObject closestStorehouse;
    storehouseScript SHScript;



    //Movement
    public GameObject townCenter;
    NavMeshAgent npcAgent;
    Animator ani;
    Vector3 zeroVelocity;
    float waitTime = 0.5f;
    public Vector3 npcDestination;

    void Start()
    {
        npcAgent = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        zeroVelocity = new Vector3(0, 0, 0);
        setTarget(townCenter);
        

    }

    void Update()
    {
        
        npcDestination = GetComponent<NavMeshAgent>().destination;
        distanceToTarget = Vector3.Distance(transform.position, target.transform.position);


        JobChanger();
    }

    

    void JobChanger()
    {

        switch (job)
        {
            case "Unemployed":
                Unemployed();
                break;
            

            case "Builder":
                Builder();
                break;
        
            case "Logger":
                Logger();
                break;

            case "Miner":
                Miner();
                break;
        }
        
        void Unemployed() 
        {
            npcAgent.isStopped = false;
            MovingAni();
            wanderTC();
       
            void wanderTC()
            {
                target = townCenter;
                float wanderX = townCenter.transform.position.x + Random.Range(-3, 3);
                float wanderZ = townCenter.transform.position.z + Random.Range(-3, 3);
                float WanderY = townCenter.transform.position.y;


                if (npcAgent.velocity == zeroVelocity)
                {
                    waitTime -= Time.deltaTime;
                }


                if (waitTime <= 0)
                {
                    Debug.Log("Wait Time Elapsed");
                    waitTime = Random.Range(10, 30);
                    npcAgent.SetDestination(new Vector3(wanderX, WanderY, wanderZ));
                }
            }
        }

        void Builder()
        {
            npcAgent.isStopped = false;
        }

        void Logger()
        {
            float detectionRange = 1.0f;

            if (load < maxLoad)
            {
                npcAgent.SetDestination(closestNode(getJob()));
                //if in detectionRage of closestNode, stop and gather
                if (Vector3.Distance(transform.position, target.transform.position) <= detectionRange)
                {
                    targetResScript = target.GetComponent<resourceScript>();
                    npcAgent.isStopped = true;
                    transform.LookAt(target.transform);
                    ChopRes();
                    gatherTimer -= Time.deltaTime;
                    if (gatherTimer <= 0)
                    {
                        Debug.Log("Gathered Log");
                        load += 1;
                        targetResScript.setLoad(targetResScript.getLoad() - 1);
                        gatherTimer = 2;
                    }
                }
                // walk to closestNode
                else
                {
                    npcAgent.isStopped = false;
                    Walk();
                }
            }

            //full of resource
            else
            {
                npcAgent.SetDestination(closestStorehouse());
                SHScript = target.GetComponent<storehouseScript>();
                if (Vector3.Distance(transform.position, target.transform.position) <= detectionRange)
                {
                    npcAgent.isStopped = true;
                
                    transform.LookAt(target.transform);
                    DropLoad();
                    unloadTimer -= Time.deltaTime;
                    if (unloadTimer <= 0)
                    {
                        Debug.Log("Dropped Wood");
                        SHScript.AddWood(load);
                        load -= load;
                        unloadTimer = 5; 
                    }
                }
                else
                {
                    npcAgent.isStopped = false;
                    WalkMaxLoad();
                }
            }
        

        }

        void Miner()
        {
            npcAgent.isStopped = false;
        }

        Vector3 closestNode(string j)
        {
            Vector3 minRes = transform.position;
            Vector3 currentPos = transform.position;
            float minDist = Mathf.Infinity;
            switch (j)
            {
                case "Logger":
                    if (UIScript.treeList.Count != 0)
                    {
                        foreach (GameObject tree_1 in UIScript.treeList)
                        {
                            float dist = Vector3.Distance(tree_1.transform.position, currentPos);
                            if (dist < minDist)
                            {
                                setTarget(tree_1);
                                minRes = tree_1.transform.position;
                                minDist = dist;
                            }

                        }
                    }
                    else 
                    {
                        setJob("Unemployed");
                    }
                    
                    break;

                //case "Miner":
                //    foreach (GameObject tree_1 in treeList)
                //    {
                //        float dist = Vector3.Distance(tree_1.transform.position, currentPos);
                //        if (dist < minDist)
                //        {
                //            minRes = tree_1.transform.position;
                //            minDist = dist;
                //        }
                //    }
                //    break;

                //case "Builder":
                //    foreach (GameObject tree_1 in treeList)
                //    {
                //        float dist = Vector3.Distance(tree_1.transform.position, currentPos);
                //        if (dist < minDist)
                //        {
                //            minRes = tree_1.transform.position;
                //            minDist = dist;
                //        }
                //    }
                //    break;
            }
            return minRes;


        }

        Vector3 closestStorehouse()
        {
            Vector3 minStorehouse = transform.position;
            Vector3 currentPos = transform.position;
            float minDist = Mathf.Infinity;
            foreach (GameObject builtObject in UIScript.builtObjectList)
            {
                if (builtObject.tag == "storehouse_lvl1")
                {
                    float dist = Vector3.Distance(builtObject.transform.position, currentPos);
                    if (dist < minDist)
                    {
                        setTarget(builtObject);
                        minStorehouse = builtObject.transform.position;
                        minDist = dist;
                    }
                }
            }
            return minStorehouse;
        }
    }


    

    //void WanderTC()
    //{
    //    target = townCenter;
    //    float wanderX = townCenter.transform.position.x + Random.Range(-3, 3);
    //    float wanderZ = townCenter.transform.position.z + Random.Range(-3, 3);
    //    float WanderY = townCenter.transform.position.y;


    //    if (npcAgent.velocity == zeroVelocity)
    //    {
    //        waitTime -= Time.deltaTime;
    //    }


    //    if (waitTime <= 0)
    //    {
    //        Debug.Log("Wait Time Elapsed");
    //        waitTime = Random.Range(10, 30);
    //        npcAgent.SetDestination(new Vector3(wanderX, WanderY, wanderZ));
    //    }
    //}

    void MovingAni()
    {
        if (npcAgent.velocity != zeroVelocity)
        {
            Walk();
        }
        else
        {
            Idle();
        }
    }

    //Animations
    void Idle()
    {
        ani.SetBool("Walk", false);
        ani.SetBool("WalkMaxLoad", false);
        ani.SetBool("ChopRes", false);
        ani.SetBool("MineRes", false);
        ani.SetBool("DropLoad", false);
    }
    void Walk()
    {
        ani.SetBool("Walk", true);
        ani.SetBool("WalkMaxLoad", false);
        ani.SetBool("ChopRes", false);
        ani.SetBool("MineRes", false);
        ani.SetBool("DropLoad", false);
    }
    void ChopRes()
    {
        ani.SetBool("Walk", false);
        ani.SetBool("ChopRes", true);
    }

    void WalkMaxLoad()
    {
        ani.SetBool("WalkMaxLoad", true);
        ani.SetBool("ChopRes", false);
        ani.SetBool("MineRes", false);
        ani.SetBool("Walk", false);
    }
    void DropLoad()
    {
        ani.SetBool("DropLoad", true);
        ani.SetBool("WalkMaxLoad", false);
        ani.SetBool("Walk", false);
    }

    void MineRes()
    {
        ani.SetBool("MineRes", true);
        ani.SetBool("Walk", false);
    }

    //Setting Values
    public void setHealth(int h)
    {
        health = h;
    }
    public void setJob(string j)
    {
        job = j;
    }
    public void setTarget(GameObject t)
    {
        target = t;
    }
    public void setGatherSpeed(int gs)
    {
        gatherSpeed = gs;
    }
    public void setMoveSpeed(float ms)
    {
        moveSpeed = ms;
    }
    public void setLoad(int l)
    {
        load = l;
    }
    public void setMaxLoad(int ml)
    {
        maxLoad = ml;
    }
    public void setLoadMaterial(string lm)
    {
        loadMaterial = lm;
    }

    //Getting Values
    public int getHealth()
    {
        return health;
    }
    public string getJob()
    {
        return job;
    }

    public GameObject getTarget()
    {
        return target;
    }
    public float getGatherSpeed()
    {
        return gatherSpeed;
    }
    public float getMoveSpeed()
    {
        return moveSpeed;
    }
    public int getLoad()
    {
        return load;
    }
    public int getMaxLoad()
    {
        return maxLoad;
    }
    public string getLoadMaterial()
    {
        return loadMaterial;
    }

    //CONSTRUCTORS
    public void civilian()
    {
        health = 10;
        job = "Unemployed";
        gatherSpeed = 10;
        moveSpeed = 0.1f;
        load = 0;
        maxLoad = 10;
    }

    public void civilian(int h, int gs, int ms, int l, int ml)
    {
        health = h;
        job = "Unemployed";
        gatherSpeed = gs;
        moveSpeed = ms;
        load = l;
        maxLoad = ml;
    }
}
