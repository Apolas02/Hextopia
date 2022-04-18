using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// on ui button
public class spawn_building_bp : MonoBehaviour
{
    public GameObject house_lvl1_bp;
    public GameObject towncenter_lvl1_bp;
    public GameObject storehouse_lvl1_bp;
    public GameObject MoveOrderNode;

    public void spawn_house_lvl1_bp()
    {
        Instantiate(house_lvl1_bp);
    }
    public void spawn_towncenter_lvl1_bp()
    {
        Instantiate(towncenter_lvl1_bp);
    }
    public void spawn_storehouse_lvl1_bp()
    {
        Instantiate(storehouse_lvl1_bp);
    }
    public void spawn_moveOrder()
    {
        Instantiate(MoveOrderNode);
    }
}
