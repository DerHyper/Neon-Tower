using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform _buildingSpawnArea;
    public static InputManager Instance { get; private set; }

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
        if (IsMouseInsideSpawnArea(out Vector2 currentPosition) && IsNotBlocked())
        {
            SpawnManager.Instance.TrySpawnBuilding(currentPosition);
        }
    }

    private bool IsNotBlocked()
    {
        return !_buildingSpawnArea.gameObject.GetComponent<SpawnArea>().IsBlocked;

    }

    public bool IsMouseInsideSpawnArea()
    {
        return IsMouseInsideSpawnArea(out Vector2 currentPosition);
    }
    public bool IsMouseInsideSpawnArea(out Vector2 currentPosition)
    {
        currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool isMouseInsideBox = RectTransformUtility.RectangleContainsScreenPoint(_buildingSpawnArea, currentPosition);
        return isMouseInsideBox;
    }
}
