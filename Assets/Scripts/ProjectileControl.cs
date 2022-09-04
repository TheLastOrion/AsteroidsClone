using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileControl : MonoBehaviour, IPoolable
{
    [Range(5f,15f)][SerializeField]private float _speed;
    private Vector3 _direction;
    private Coroutine _despawnCoroutine;
    private Coroutine _moveCoroutine;
    private Collider _collider;
    private Rigidbody _rigidbody;
    [SerializeField] private float _timer;
    
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
        _moveCoroutine = StartCoroutine(MoveCoroutine(GameController.Instance.Fighter.transform.up));
    }

    public void SetMovement(Vector3 direction)
    {
        // _rigidbody.AddRelativeForce(direction.normalized * _speed);
        
    }
    private void OnDisable()
    {        
        Debug.Log("Desubscribing from OnBorderExit Event!");
        GameEvents.BorderExit -=GameEventsOnBorderExit;
        StopCoroutine(_despawnCoroutine);
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
