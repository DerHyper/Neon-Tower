using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class HeightManager : MonoBehaviour
{
    public RectTransform HeigtScoreUI;
    public TMP_Text HeightScoreLabel;
    public GameObject cameraCenterPonit;
    [SerializeField]
    private float _cameraMovementSpeed = 0.1f;
    private float _cameraStartHeight;
    private const int RayCount = 20;
    private void Start() 
    {
        _cameraStartHeight = cameraCenterPonit.transform.position.y; 
    }
    private void FixedUpdate()
    {
        float height = GetHeighestPonit();
        UpdateHeightUI(height);
        UpdateCameraPosition(height);
        int roundedHeight = (int)Math.Round(height); // Round for uniformity with score label
        MilestoneManager.Instance.CheckMilestoneReached(roundedHeight);
    }

    private void UpdateCameraPosition(float height)
    {
        // if (height < _cameraMovementStart) return;
        Vector3 currentPosition = cameraCenterPonit.transform.position;
        Vector3 goalPosition = new(currentPosition.x, Math.Max(height,_cameraStartHeight),-10);
        //Vector3 lerpPosition = Vector3.Lerp(currentPosition, goalPosition, _cameraMovementSpeed);
        cameraCenterPonit.transform.position = goalPosition;
    }

    private float GetHeighestPonit()
    {
        float yPoint = GetRayStartPoint();
        float height = FindHeighestRayHit(yPoint);
        return height;
    }

    private void UpdateHeightUI(float height)
    {
        Vector2 uiPosition = HeigtScoreUI.anchoredPosition;
        HeigtScoreUI.anchoredPosition = new(uiPosition.x, height+0.1f);
        string scoreText = "Height: " + (Math.Round(height)).ToString() + "m";
        HeightScoreLabel.text = scoreText;
    }

    private float FindHeighestRayHit(float yPoint)
    {
        float leftLimit = -5f;
        float rightLimit = 5f;
        float step = (Math.Abs(leftLimit) + Math.Abs(rightLimit)) / RayCount;
        List<float> hitHeights = new();


        for (float xPoint = leftLimit; xPoint < rightLimit; xPoint += step)
        {
            Vector2 RayStartPosition = new(xPoint, yPoint);
            RaycastHit2D hit = Physics2D.Raycast(RayStartPosition, Vector2.down);

            // Check: Is Building, Is not falling
            if (hit.collider == null) continue;
            if (!hit.collider.gameObject.tag.Equals("Building")) continue;
            if (hit.collider.gameObject.GetComponent<PlacedBuilding>().IsFalling) continue;

            hitHeights.Add(hit.point.y);

            Debug.DrawRay(RayStartPosition, Vector2.down*100);
        }

        return hitHeights.DefaultIfEmpty(0).Max();
    }

    private float GetRayStartPoint()
    {
        float yOffset = 10f;
        float yPoint = GameObject.FindGameObjectWithTag("MainCamera").transform.position.y + yOffset;
        return yPoint;
    }
}
