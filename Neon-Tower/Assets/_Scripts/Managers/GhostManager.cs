using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform _buildingSpawnArea;
    [SerializeField]
    private Color _activeGhostColor;
    [SerializeField]
    private Color _inactiveGhostColor;
    private Color _currentGhostColor;
    private GameObject _currentGhost;
    public static GhostManager Instance { get; private set; }

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
    private void Update()
    {
        if (_currentGhost)
        {
            UpdateGhostPosition();
            UpdateGhostColor();
        }
    }

    private void UpdateGhostColor()
    {
        if (InputManager.Instance.IsMouseInsideSpawnArea())
        {
            ChangeGhostColor(_activeGhostColor);
        }
        else
        {
            ChangeGhostColor(_inactiveGhostColor);
        }
    }

    public void ActivateGhost()
    {
        ChangeGhostColor(_activeGhostColor);
    }
    public void DeactivateGhost()
    {
        ChangeGhostColor(_inactiveGhostColor);
    }

    public void UpdateCurrentBuilding(BuildingInfo buildingInfo)
    {
        ChangeGhost(buildingInfo.BuildingPrefab);
    }

    private void ChangeGhost(GameObject buildingPrefab)
    {
        Destroy(_currentGhost);
        GameObject visuals = buildingPrefab.GetComponentInChildren<SpriteRenderer>().gameObject;
        Debug.Log("Instantiate Visuals: " + visuals);
        _currentGhost = Instantiate(visuals);
        _currentGhostColor = Color.white; // Update current color to match
        DeactivateGhost();
    }

    private void ChangeGhostColor(Color color)
    {
        if (_currentGhostColor.Equals(color)) return;
        _currentGhostColor = color;
        SpriteRenderer[] sprites = _currentGhost.GetComponents<SpriteRenderer>();
        foreach (var sprite in sprites)
        {
            sprite.color = color;
        }
    }

    private void UpdateGhostPosition()
    {
        Vector3 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _currentGhost.transform.position = new(cameraPosition.x, cameraPosition.y, 0); // Clip in front of Camera
    }
}
