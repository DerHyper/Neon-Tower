using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInfo : MonoBehaviour
{
    public GameObject BuildingPrefab;
    public EcoUnit GeneratesUnit;
    public int GeneratesAmount;
    public List<EcoUnit> ConsumesUnits;
    public List<int> ConsumesAmounts;
}
