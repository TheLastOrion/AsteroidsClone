using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // private Rigidbody _rigidbody;
    // private Collider _collider;

    private void Awake()
    {
        // _rigidbody = GetComponent<Rigidbody>();
        // _collider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player Hit!");
            GameEvents.FirePlayerHitByAsteroid(otherCollider);
        }
    }
    
    
}
