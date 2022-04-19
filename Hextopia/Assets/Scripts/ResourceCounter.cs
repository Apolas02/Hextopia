using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCounter : MonoBehaviour
{
    private int population = 0;
    private int populationLimit = 0;
    private int wood = 0;
    private int stone = 0;

    //Resource Nodes
    public List<GameObject> treeList = new List<GameObject>();
    public List<GameObject> stoneList = new List<GameObject>();


    private void Start()
    {
        PopulateResourceLists();
    }

    void PopulateResourceLists()
    {
        foreach (GameObject tree in GameObject.FindGameObjectsWithTag("tree"))
        {
            treeList.Add(tree);
        }
        foreach (GameObject stone in GameObject.FindGameObjectsWithTag("stone"))
        {
            stoneList.Add(stone);
        }
    }

    // Sets
    public void SetPopulation(int p)
    {
        population = p;
    }

    public void SetPopulationLimit(int pl)
    {
        populationLimit = pl;
    }

    public void SetWood(int w)
    {
        wood = w;
    }

    public void SetStone(int s)
    {
        stone = s;
    }


    // Adds
    public void AddPopulation(int p)
    {
        population += p;
    }

    public void AddPopulationLimit(int pl)
    {
        populationLimit += pl;
    }

    public void AddWood(int w)
    {
        wood += w;
    }

    public void AddStone(int s)
    {
        stone += s;
    }



    // Gets
    public int GetPopulation()
    {
        return population;
    }

    public int GetPopulationLimit()
    {
        return populationLimit;
    }

    public int GetWood()
    {
        return wood;
    }

    public int GetStone()
    {
        return stone;
    }








}
