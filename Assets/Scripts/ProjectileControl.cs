using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileControl : MonoBehaviour, IPoolable
{
    [Range(5f,15f)][SerializeField]private float _speed;
    [SerializeField] private float _timer;

    private Vector3 _direction;
    private Coroutine _despawnCoroutine;
    private Coroutine _moveCoroutine;
    private Collider _collider;
    private Rigidbody _rigidbody;

    private void Start()
    {
        if (_collider == null)
            _collider = GetComponent<Collider>();
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        GameEvents.BorderExit += GameEventsOnBorderExit;
        _despawnCoroutine = StartCoroutine("StartTimerCountdownCoroutine");
    }

    public void SetMovement(Vector3 direction)
    {
        _moveCoroutine = StartCoroutine(MoveCoroutine(direction));
    }
    private void OnDisable()
    {        
        Debug.Log("Desubscribing from OnBorderExit Event!");
        GameEvents.BorderExit -=GameEventsOnBorderExit;
        // if(_despawnCoroutine != null)
            StopCoroutine(_despawnCoroutine);
        // if(_moveCoroutine != null)
            StopCoroutine(_moveCoroutine);
    }

    private void GameEventsOnBorderExit(BorderType borderType, Collider teleportCollider)
    {
        if (teleportCollider == _collider)
        {
            transform.position = GameUtils.FindTeleportPlace(transform, borderType);
        }    
    }
    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Hit!");
            GameEvents.FireAsteroidHitByProjectile(_collider, otherCollider);
            DeSpawn();
        }
    }

    private IEnumerator StartTimerCountdownCoroutine()
    {
        float temp = _timer;
        yield return new WaitForSeconds(temp);
        DeSpawn();
        
    }

    private IEnumerator MoveCoroutine(Vector3 direction)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            transform.Translate(direction.normalized * (_speed * Time.deltaTime));
        }
    }
    public void DeSpawn()
    {
        Debug.LogFormat("Despawning {0}", gameObject.name);
        ObjectPooler.Instance.RecyclePooledObject(gameObject);
    }
}
