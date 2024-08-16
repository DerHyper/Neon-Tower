using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform _buildingSpawnArea;
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Mouse 0 Pressed.");
            TryBuildBuilding();
        }
    }

    private void TryBuildBuilding()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool isMouseInsideBox = RectTransformUtility.RectangleContainsScreenPoint(_buildingSpawnArea, mousePos);
        Debug.Log($"Mouse {mousePos} in {_buildingSpawnArea} is {isMouseInsideBox}.");
        if(isMouseInsideBox)
        {
            SpawnManager.Instance.BuildBuilding(mousePos);
        }
    }
}
