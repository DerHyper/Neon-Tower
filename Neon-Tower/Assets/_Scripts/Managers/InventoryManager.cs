using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public void Select(BuildingInfo Building)
    {
        SpawnManager.Instance.currentBuilding = Building;
        GhostManager.Instance.UpdateCurrentBuilding(Building);
    }
}
