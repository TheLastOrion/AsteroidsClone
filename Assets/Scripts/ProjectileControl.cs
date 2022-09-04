using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileControl : MonoBehaviour, IPoolable
{
    private int m_speed;
    private Vector3 m_direction;
    private Collider m_collider;

    private void Start()
    {
        if (m_collider == null)
            m_collider = GetComponent<Collider>();
    }
    private void OnEnable()
    {
        GameEvents.BorderExit +=GameEventsOnBorderExit;
    }
    private void OnDisable()
    {        
        GameEvents.BorderExit -=GameEventsOnBorderExit;
    }

    private void GameEventsOnBorderExit(BorderType borderType, Collider teleportCollider)
    {
        if (teleportCollider == m_collider)
        {
            transform.position = CalculationUtils.FindTeleportPlace(transform, borderType);
        }    
    }
    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Hit!");
            GameEvents.FireAsteroidHitByProjectile(m_collider, otherCollider);
            DeSpawn();

        }
    }

    public void DeSpawn()
    {
        Debug.LogFormat("Despawning {0}", gameObject.name);
        ObjectPooler.Instance.RecyclePooledObject(gameObject);
    }
}
