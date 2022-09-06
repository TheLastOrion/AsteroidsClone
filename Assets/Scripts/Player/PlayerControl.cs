using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player Hit!");
            GameEvents.FirePlayerHitByAsteroid(otherCollider, otherCollider.GetComponent<AsteroidControl>());
        }
    }
}
