using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _fragments;
    [SerializeField]
    private float _energy;
    
    void Start()
    {
        Vector2 currentPosition = new Vector2(gameObject.transform.position.x,gameObject.transform.position.y);
        foreach (Sprite fragment in _fragments)
        {
            Vector2 distanceFromCenter = UnityEngine.Random.insideUnitCircle;
            Vector2 randomStartPosition = currentPosition + distanceFromCenter;

            // Instantiate new GameObject
            GameObject instantiatedFragment = new GameObject("ExplosionFragment");
            instantiatedFragment.transform.position = randomStartPosition;
            instantiatedFragment.transform.parent = gameObject.transform;
            instantiatedFragment.AddComponent<SpriteRenderer>().sprite = fragment;
            instantiatedFragment.AddComponent<Rigidbody2D>().AddForce(distanceFromCenter*_energy);
            instantiatedFragment.AddComponent<ExplosionFragment>();
        }

        Invoke(nameof(SelfDestroy), 2);
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
