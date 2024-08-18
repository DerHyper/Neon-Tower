using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    [SerializeField]
    private float _timeout;
    public bool IsBlocked = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        IsBlocked = true;
    }

    // private void OnTriggerStay2D(Collider2D other) {
    //     IsBlocked = true;
    // }

    private void OnTriggerExit2D(Collider2D other)
    {
        StartCoroutine(UnblockAfterTime(_timeout));
    }

    private IEnumerator UnblockAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        IsBlocked = false;
    }

}
