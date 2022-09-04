using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class AsteroidControl : MonoBehaviour, IPoolable, IAutoMoveable
{
    [FormerlySerializedAs("m_speed")] [SerializeField] private float _speed;

    private Vector3 _direction;
    private Coroutine _moveCoroutine;
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
        if(_moveCoroutine != null)StopCoroutine(_moveCoroutine);
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
    public void SetMovement(Vector3 direction)
    {
        _moveCoroutine = StartCoroutine(MoveCoroutine(direction));
    }

    public void SetRandomSpeed()
    {
        _speed = Random.Range(1f, 3f);
    }
    private IEnumerator MoveCoroutine(Vector3 direction)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            transform.Translate(direction.normalized * (_speed * Time.deltaTime));
        }    
    }

    public AsteroidSize GetAsteroidSize()
    {
        return _asteroidSize;
    }
    public void DeSpawn()
    {
        Debug.LogFormat("Despawning {0}", gameObject.name);
        ObjectPooler.Instance.RecyclePooledObject(gameObject);
    }
}
