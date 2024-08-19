using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EcoUnit : MonoBehaviour
{
    public int Current { get; private set; }
    [SerializeField]
    private TMP_Text _amountUI;

    private void Start() {
        UpdateUI();
    }

    public bool TryAdd(int amount)
    {
        Current += amount;
        UpdateUI();
        return true;
    }

    public bool CanSubtract(int amount)
    {
        return Current - amount >= 0;
    }

    public bool TrySubtract(int amount)
    {
        if (Current - amount < 0)
        {
            return false;
        }

        Current -= amount;
        Debug.Log("ScoreManager: "+ScoreManager.Instance);
        Debug.Log("amount: "+amount);
        //ScoreManager.Instance.AddScore(amount);
        UpdateUI();
        return true;
    }


    private void UpdateUI()
    {
        _amountUI.text = Current.ToString();
    }
}
