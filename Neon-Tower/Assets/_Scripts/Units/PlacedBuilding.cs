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

    private void OnMouseEnter()
    {
        // Change Color to destroy
        GetComponentInChildren<SpriteRenderer>().color = _destroyColor;
    }

    private void OnMouseExit()
    {
        // Change Color to normal
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }

    private void OnMouseOver()
    {
        // Leftclick to Destroy
        if (Input.GetMouseButton(0) && !_isStatic)
        {
            SelfDestroy();
        }
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
        Destroy(gameObject);
    }

}
