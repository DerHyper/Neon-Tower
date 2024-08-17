using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomicsManager : MonoBehaviour
{
    public static EconomicsManager Instance { get; private set; }
    [SerializeField]
    public EcoUnit Games;
    public EcoUnit Drinks;
    public EcoUnit Residents;
    public EcoUnit Ramen;

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

    private void Start()
    {
        Residents.TryAdd(100);
    }

    public bool TryPayBuildCosts(BuildingInfo building)
    {
        // Check if enough units are available
        for (int i = 0; i < building.ConsumesUnits.Count; i++)
        {
            EcoUnit unit = building.ConsumesUnits[i];
            int amount = building.ConsumesAmounts[i];
            if (!unit.CanSubtract(amount))
            {
                return false;
            }
        }

        // Pay with units
        for (int i = 0; i < building.ConsumesUnits.Count; i++)
        {
            EcoUnit unit = building.ConsumesUnits[i];
            int amount = building.ConsumesAmounts[i];
            unit.TrySubtract(amount);
        }

        TryAdd(building.GeneratesUnit, building.GeneratesAmount);

        return true;
    }


    public bool TryAdd(EcoUnit unit, int amount)
    {
        return unit.TryAdd(amount);
    }

    public bool TrySubtract(EcoUnit unit, int amount)
    {
        return unit.TrySubtract(amount);
    }
}
