using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlacedBuilding : MonoBehaviour
{
    // Rest of the Eco Stuff, might get readded later
    // [HideInInspector]
    // public List<EcoUnit> ConsumedUnits;
    // [HideInInspector]
    // public List<int> ConsumedAmounts;
    [HideInInspector]
    public EcoUnit GeneratesUnit;
    [HideInInspector]
    public int GeneratesAmount;
    public bool IsFalling { get; private set; }
    [SerializeField]
    private float ScorePenaltyFactor = 2;
    private bool _isStatic = false;
    private Color _destroyColor = Color.red;
    private void Start()
    {
        IsFalling = true;
        gameObject.layer = 2; // Ignore Raycast
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isStatic && other.gameObject.tag.Equals("DestructionZone"))
        {
            SelfDestroy();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Building colliding mid-air are still falling
        if (other.gameObject.tag.Equals("Building") && other.gameObject.GetComponent<PlacedBuilding>().IsFalling)
        {
            return;
        }

        // Switch out of falling State
        IsFalling = false;
        gameObject.layer = 0; // Default (Raycasts can now hit for height calculation)

        // If fast enough play HitSound
        List<ContactPoint2D> contactPoints = new List<ContactPoint2D>();
        other.GetContacts(contactPoints);
        float impulse = 0;
        foreach (ContactPoint2D point in contactPoints)
        {
            impulse += point.normalImpulse;
        }
        SFXManager.Instance.PlayBuildingImpact(impulse);
    }

    private void OnMouseEnter()
    {
        // Change Color to destroy
        if(!_isStatic)
        {
            GetComponentInChildren<SpriteRenderer>().color = _destroyColor;
        }
    }

    private void OnMouseExit()
    {
        // Change Color to normal
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }

    private void OnMouseOver()
    {
        // Leftclick to Destroy
        if (Input.GetMouseButtonDown(0) && !_isStatic)
        {
            SelfDestroy();
        }
    }

    public void Freeze()
    {
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        _isStatic = true;
    }

    private void RefundCostsAndScore()
    {
        // Rest of the Eco Stuff, might get readded later
        // // Refund units 
        // for (int i = 0; i < ConsumedUnits.Count; i++)
        // {
        //     EcoUnit unit = ConsumedUnits[i];
        //     int amount = ConsumedAmounts[i];
        //     unit.TrySubtract(amount);
        //     ScoreManager.Instance.RemoveScore(amount * ScorePenaltyFactor);
        // }

        EconomicsManager.Instance.TrySubtract(GeneratesUnit,GeneratesAmount);
        ScoreManager.Instance.RemoveScore(GeneratesAmount * ScorePenaltyFactor);
    }

    private void SelfDestroy()
    {
        RefundCostsAndScore();
        VFXManager.Instance.BuildingExplosion(gameObject.transform.position);
        SFXManager.Instance.PlayBuildingDestroy();
        Destroy(gameObject);
    }

}
