using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidControl : MonoBehaviour, IPoolable
{
    [SerializeField] private float m_speed;

    private Vector3 m_direction;

    public Vector3 Direction
    {
        get => m_direction;
        set => m_direction = value;
    }

    private Collider m_collider;
    [SerializeField] private AsteroidSize m_asteroidSize;
    void Start()
    {
        if (m_collider == null)
        {
            m_collider = GetComponent<Collider>();
        }
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        GameEvents.AsteroidHitByProjectile += GameEventsOnAsteroidHitByProjectile;
    }

    private void OnDisable()
    {
        GameEvents.AsteroidHitByProjectile -= GameEventsOnAsteroidHitByProjectile;
    }

    private void GameEventsOnAsteroidHitByProjectile(Collider projectileCollider, Collider asteroidCollider)
    {
        if (asteroidCollider == m_collider)
        {
            DeSpawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AsteroidSize GetAsteroidSize()
    {
        return m_asteroidSize;
    }

    
    public void DeSpawn()
    {
        Debug.LogFormat("Despawning {0}", gameObject.name);
        ObjectPooler.Instance.RecyclePooledObject(gameObject);    }
}
