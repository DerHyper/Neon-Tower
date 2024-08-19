using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomicsManager : MonoBehaviour
{
    public static EconomicsManager Instance { get; private set; }
    public EcoUnit Games;
    public EcoUnit Drinks;
    public EcoUnit Residents;
    public EcoUnit Ramen;
    [SerializeField]
    private int _startUnitAmount;

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
        Games.TryAdd(_startUnitAmount);
        Drinks.TryAdd(_startUnitAmount);
        Ramen.TryAdd(_startUnitAmount);
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
                Debug.Log("Not Enougth: ", building.ConsumesUnits[i]);
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
        ScoreManager.Instance.AddScore(building.GeneratesAmount);

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
