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
    private const int RayCount = 20;
    private void FixedUpdate()
    {
        float yPoint = GetRayStartPoint();
        float height = FindHeighestRayHit(yPoint);
        UpdateHeightUI(height);
        Debug.Log("Heighest Ponit:"+ height);
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
            if (hit.collider.gameObject.tag != "Building") continue;
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
