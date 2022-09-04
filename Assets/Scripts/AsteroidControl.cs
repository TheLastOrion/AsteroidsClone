using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class AsteroidControl : MonoBehaviour, IPoolable, IAutoMoveable
{
    [FormerlySerializedAs("m_speed")] [SerializeField] private float _speed;
    private Transform _moveTransform;
    private Vector3 _direction;
    private Coroutine _moveCoroutine;
    private Coroutine _selfCheckCoroutine;
    public Vector3 Direction
    {
        get => _direction;
        set => _direction = value;
    }

    private Collider _collider;
    [SerializeField] private AsteroidSize _asteroidSize;
    void Start()
    {
        _moveTransform = transform.parent;
        if (_collider == null)
        {
            _collider = GetComponent<Collider>();
        }
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        GameEvents.AsteroidHitByProjectile += GameEventsOnAsteroidHitByProjectile;
        GameEvents.BorderExit += GameEventsOnBorderExit;
        _selfCheckCoroutine = StartCoroutine(SelfDestructIfTooFar());
    }

    

    private void OnDisable()
    {
        GameEvents.AsteroidHitByProjectile -= GameEventsOnAsteroidHitByProjectile;
        GameEvents.BorderExit -= GameEventsOnBorderExit;

        if(_moveCoroutine != null)StopCoroutine(_moveCoroutine);
        if (_selfCheckCoroutine != null) StopCoroutine(_selfCheckCoroutine);
    }

    private void GameEventsOnAsteroidHitByProjectile(Collider projectileCollider, Collider asteroidCollider)
    {
        if (asteroidCollider == _collider)
        {
            DeSpawn();
        }
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
        if (otherCollider.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Hit!");
            GameEvents.FirePlayerHitByAsteroid(_collider);
            DeSpawn();
        }
    }
    public void SetMovement(Vector3 direction)
    {
        if (!gameObject.activeInHierarchy)
            return;
        _moveCoroutine = StartCoroutine(MoveCoroutine(direction));
    }

    public void SetRandomSpeed()
    {
        _speed = Random.Range(1f, 3f);
    }
    private IEnumerator MoveCoroutine(Vector3 direction)
    {
        float randX = Random.Range(-1f, 1f);
        float randY = Random.Range(-1f, 1f);
        Debug.LogFormat("RandX: {0}  RandY: {1}", randX, randY);

        while (true)
        {
            yield return new WaitForFixedUpdate();
            
            // transform.Translate(direction.normalized * (_speed * Time.deltaTime));
            _moveTransform.Translate(new Vector3(randX,randY , 0) * (_speed * Time.deltaTime));
        }    
    }

    private IEnumerator SelfDestructIfTooFar()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (Vector3.Distance(transform.position, Vector3.zero) >= Constants.SELF_DESTRUCT_DISTANCE)
            {
                GameEvents.FireAsteroidSelfDestructed(_collider);
                DeSpawn();
            }
        }
    }
    public AsteroidSize GetAsteroidSize()
    {
        return _asteroidSize;
    }
    public void DeSpawn()
    {
        Debug.LogFormat("Despawning {0}", _moveTransform.gameObject.name);
        ObjectPooler.Instance.RecyclePooledObject(_moveTransform.gameObject);
    }
    
}
