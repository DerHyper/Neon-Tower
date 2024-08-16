using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }
    public GameObject currentBuilding;
    
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


    public void BuildBuilding(Vector2 position)
    {
        Debug.Log($"Build Block at {position}.");
        Vector3 buildPosition = position;
        Quaternion rotation = Quaternion.identity;
        Instantiate(currentBuilding, buildPosition, rotation, BuildingParent);
    }
}
