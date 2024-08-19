using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    private Vector3 offset;

    private void Start()
    {
        Vector3 targetPosition = target.transform.position;
        offset = gameObject.transform.position - targetPosition;
    }

    private void Update()
    {
        Vector3 targetPosition = target.transform.position;
        gameObject.transform.position = targetPosition + offset;
    }

}
