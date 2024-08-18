using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour
{
    public static WindManager Instance;
    public float _windIntensity = 1;
    [SerializeField]
    private float _windInterval = 1;
    [SerializeField]
    private float _windTimeout = 1;
    [SerializeField]
    private float _windUplift = 1;
    [SerializeField]
    private float WindIncreaseAfterMilestone = 0.2f;
    private WindDirection currentWindDirection = WindDirection.None;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        StartCoroutine(WindLoopRight());
    }

    public void IncreaseWindInsensity()
    {
        _windIntensity += WindIncreaseAfterMilestone;
    }

    private void FixedUpdate()
    {
        if (currentWindDirection == WindDirection.Right)
        {
            InitCurrent(_windIntensity);
        }
        else if (currentWindDirection == WindDirection.Left)
        {
            InitCurrent(-_windIntensity);
        }
    }

    private IEnumerator WindLoopRight()
    {
        currentWindDirection = WindDirection.Right;
        yield return new WaitForSeconds(_windInterval);
        currentWindDirection = WindDirection.None;
        yield return new WaitForSeconds(_windTimeout);
        StartCoroutine(WindLoopLeft());
    }

    private IEnumerator WindLoopLeft()
    {
        currentWindDirection = WindDirection.Left;
        yield return new WaitForSeconds(_windInterval);
        currentWindDirection = WindDirection.None;
        yield return new WaitForSeconds(_windTimeout);
        StartCoroutine(WindLoopRight());
    }

    private void InitCurrent(float intensity)
    {
        PlacedBuilding[] buildings = FindObjectsOfType<PlacedBuilding>();

        foreach (var building in buildings)
        {
            if (building.IsFalling)
            {
                continue;
            }
            building.gameObject.GetComponent<Rigidbody2D>().AddForce(new(intensity, _windUplift));
        }
    }

    enum WindDirection
    {
        Left,
        Right,
        None
    }
}
