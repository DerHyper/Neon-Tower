using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedBuilding : MonoBehaviour
{
    [HideInInspector]
    public List<EcoUnit> ConsumedUnits;
    [HideInInspector]
    public List<int> ConsumedAmounts;
    public bool IsFalling { get; private set; }

    [SerializeField]
    private float ScorePenaltyFactor = 2;
    private bool _isStatic = false;
    private void Start()
    {
        IsFalling = true;
        gameObject.layer = 2; // Ignore Raycast
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isStatic && other.gameObject.tag.Equals("DestructionZone"))
        {
            DestroySelf();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Building colliding mid-air are still falling
        if (other.gameObject.tag.Equals("Building") && other.gameObject.GetComponent<PlacedBuilding>().IsFalling)
        {
            return;
        }
        IsFalling = false;
        gameObject.layer = 0; // Default (Raycasts can now hit for height calculation)
    }

    public void Freeze()
    {
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        _isStatic = true;
    }


    private void DestroySelf()
    {
        Debug.Log("BOOM");

        RefundCostsAndScore();

        Destroy(gameObject);
    }


    private void RefundCostsAndScore()
    {
        // Refund units
        for (int i = 0; i < ConsumedUnits.Count; i++)
        {
            EcoUnit unit = ConsumedUnits[i];
            int amount = ConsumedAmounts[i];
            unit.TryAdd(amount);
            ScoreManager.Instance.RemoveScore(amount * ScorePenaltyFactor);
        }
    }
}
