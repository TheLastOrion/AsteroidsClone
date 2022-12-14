using System.Collections;
using UnityEngine;

public class ProjectileControl : MonoBehaviour, IPoolable, IAutoMoveable
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
        GameEvents.AsteroidHitByProjectile += GameEventsOnAsteroidHitByProjectile;
        GameEvents.GameOver += GameEventsOnGameOver;
        _despawnCoroutine = StartCoroutine("StartTimerCountdownCoroutine");
    }
    private void OnDisable()
    {        
        Debug.Log("Desubscribing from OnBorderExit Event!");
        GameEvents.BorderExit -=GameEventsOnBorderExit;
        GameEvents.AsteroidHitByProjectile -= GameEventsOnAsteroidHitByProjectile;
        GameEvents.GameOver -= GameEventsOnGameOver;
        if(_despawnCoroutine != null)
            StopCoroutine(_despawnCoroutine);
        if(_moveCoroutine != null)
            StopCoroutine(_moveCoroutine);
    }
    private void GameEventsOnGameOver()
    {
        DeSpawn();
    }

    private void GameEventsOnAsteroidHitByProjectile(Collider projectileCollider, Collider asteroidCollider, AsteroidControl asteroidControl, Transform asteroidContainerTransform)
    {
        if (projectileCollider == _collider)
        {
            DeSpawn();
        }
    }

    public void SetMovement(Vector3 direction)
    {
        _moveCoroutine = StartCoroutine(MoveCoroutine(direction));
    }
    

    private void GameEventsOnBorderExit(BorderType borderType, Collider teleportCollider)
    {
        if (teleportCollider == _collider)
        {
            transform.position = GameUtils.FindTeleportPlace(transform, borderType);
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
