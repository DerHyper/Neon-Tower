using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public void Select(GameObject Building)
    {
        SpawnManager.Instance.currentBuilding = Building;
    }
}
