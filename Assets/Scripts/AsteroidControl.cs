using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AsteroidControl : MonoBehaviour, IPoolable
{
    [SerializeField] private float m_speed;

    private Vector3 _direction;

    public Vector3 Direction
    {
        get => _direction;
        set => _direction = value;
    }

    private Collider _collider;
    [SerializeField] private AsteroidSize _asteroidSize;
    void Start()
    {
        if (_collider == null)
        {
            _collider = GetComponent<Collider>();
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
        if (asteroidCollider == _collider)
        {
            DeSpawn();
        }
    }
    
    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Hit!");
            GameEvents.FirePlayerHitByAsteroid(_collider);
            DeSpawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AsteroidSize GetAsteroidSize()
    {
        return _asteroidSize;
    }

    
    public void DeSpawn()
    {
        Debug.LogFormat("Despawning {0}", gameObject.name);
        ObjectPooler.Instance.RecyclePooledObject(gameObject);    }
}
