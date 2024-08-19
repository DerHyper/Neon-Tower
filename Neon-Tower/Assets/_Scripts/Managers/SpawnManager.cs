using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }
    public BuildingInfo currentBuilding;
    
    [SerializeField]
    private Transform BuildingParent; // This should be a child of "Environment"

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    public void TrySpawnBuilding(Vector2 position)
    {
        if(EconomicsManager.Instance.TryPayBuildCosts(currentBuilding))
        {
            SpawnCurrentBuilding(position);
        }
    }

    private void SpawnCurrentBuilding(Vector2 position)
    {
        Vector3 buildPosition = position;
        Quaternion rotation = Quaternion.identity;
        PlacedBuilding building = Instantiate(currentBuilding.BuildingPrefab, buildPosition, rotation, BuildingParent).GetComponent<PlacedBuilding>();
        // Rest of the Eco Stuff, might get readded later
        // building.ConsumedUnits = currentBuilding.ConsumesUnits;
        // building.ConsumedAmounts = currentBuilding.ConsumesAmounts;
        building.GeneratesAmount = currentBuilding.GeneratesAmount;
        building.GeneratesUnit = currentBuilding.GeneratesUnit;

    }
}
