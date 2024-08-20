using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionFragment : MonoBehaviour
{
    private float _shrinkingFactor = 1f;
    private float _randomShrinkingAmount;


    private void Start()
    {
        _randomShrinkingAmount = UnityEngine.Random.Range(.2f, 1f) * _shrinkingFactor;
    }

    private void Update()
    {
        float _shrinkingAmount = _randomShrinkingAmount * Time.deltaTime;
        if (gameObject.transform.localScale.x - _shrinkingAmount <= 0)
        {
            gameObject.transform.localScale = new(0, 0, 0);
        }
        else
        {
            gameObject.transform.localScale = gameObject.transform.localScale - Vector3.one*_shrinkingAmount;
        }
    }
}
