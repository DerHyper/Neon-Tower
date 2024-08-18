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
        if(IsMouseInsideSpawnArea(out Vector2 currentPosition) && IsNotBlocked())
        {
            SpawnManager.Instance.TrySpawnBuilding(currentPosition);
        }
    }

    private bool IsNotBlocked()
    {
        return !_buildingSpawnArea.gameObject.GetComponent<SpawnArea>().IsBlocked;
        
    }

    private bool IsMouseInsideSpawnArea(out Vector2 currentPosition)
    {
        currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool isMouseInsideBox = RectTransformUtility.RectangleContainsScreenPoint(_buildingSpawnArea, currentPosition);
        return isMouseInsideBox;
    }
}
