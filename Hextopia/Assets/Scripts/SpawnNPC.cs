using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNPC : MonoBehaviour
{
    ResourceCounter resourceCounter;
    CivilianScript civilianScript;
    public clickTarget clickTarget;

    

    public GameObject[] npcs = { };
    public void SpawnCivilian()
    {
        
        GameObject newCiv;
        GameObject playerTarget = clickTarget.clickedTarget;

        resourceCounter = GetComponent<ResourceCounter>();





        int population = resourceCounter.GetPopulation();
        int populationLimit = resourceCounter.GetPopulationLimit();
        int randomCiv = Random.Range(0, npcs.Length);
        int randomPosition = Random.Range(0, 4);
        

        Vector3[] spawnPos = { playerTarget.transform.GetChild(0).position,
                               playerTarget.transform.GetChild(1).position,
                               playerTarget.transform.GetChild(2).position,
                               playerTarget.transform.GetChild(3).position };

        Quaternion[] spawnRot = { playerTarget.transform.GetChild(0).rotation,
                                  playerTarget.transform.GetChild(1).rotation,
                                  playerTarget.transform.GetChild(2).rotation,
                                  playerTarget.transform.GetChild(3).rotation };

        if (population < populationLimit)
        {
            Debug.Log("spawn civilian");
            newCiv = Instantiate(npcs[randomCiv], spawnPos[randomPosition], spawnRot[randomPosition]);
            civilianScript = newCiv.GetComponent<CivilianScript>();
            civilianScript.townCenter = playerTarget;
            civilianScript.civilian();
            resourceCounter.AddPopulation(1);

            /* adds basic values for health etc
            newCiv.AddComponent<civilianScript>();

            // add and set capsule collider 
            newCiv.AddComponent<CapsuleCollider>();
            newCiv.GetComponent<CapsuleCollider>().center = new Vector3(0.0009801995f, 0.4998914f, 0.0213078f);
            newCiv.GetComponent<CapsuleCollider>().radius = 0.2374798f;
            newCiv.GetComponent<CapsuleCollider>().height = 1.029353f;

            // add Rigidbody and set constraints
            newCiv.AddComponent<Rigidbody>();
            newCiv.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX |
                                                        RigidbodyConstraints.FreezeRotationY |
                                                        RigidbodyConstraints.FreezeRotationZ;

            // add animator 
            newCiv.AddComponent<Animator>();
            anim = civScript.GetComponent<Animator>();
            // adds animator controller from NPC in HideyHole
            anim.runtimeAnimatorController = npc.GetComponent<Animator>().runtimeAnimatorController;

            // add Nav Mesh Agent
            newCiv.AddComponent<NavMeshAgent>();*/
        }
    }
}
