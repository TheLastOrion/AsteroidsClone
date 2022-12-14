using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class AsteroidControl : MonoBehaviour, IPoolable, IAutoMoveable
{
    [FormerlySerializedAs("m_speed")] [SerializeField] private float _speed;
    
    private Transform _containerMoveTransform;
    private Coroutine _moveCoroutine;
    private Coroutine _selfCheckCoroutine;
    private Collider _collider;
    [SerializeField] private AsteroidSize _asteroidSize;
    void Start()
    {
        _containerMoveTransform = transform.parent;
        if (_collider == null)
        {
            _collider = GetComponentInChildren<Collider>();
        }
    }
    void OnEnable()
    {
        GameEvents.BorderExit += GameEventsOnBorderExit;
        GameEvents.PlayerHitByAsteroid += GameEventsOnPlayerHitByAsteroid;
        GameEvents.GameOver += GameEventsOnGameOver;
        _selfCheckCoroutine = StartCoroutine(SelfDestructIfTooFar());
    }
    private void OnDisable()
    {
        GameEvents.BorderExit -= GameEventsOnBorderExit;
        GameEvents.PlayerHitByAsteroid -= GameEventsOnPlayerHitByAsteroid;
        GameEvents.GameOver -= GameEventsOnGameOver;

        if(_moveCoroutine != null)StopCoroutine(_moveCoroutine);
        if (_selfCheckCoroutine != null) StopCoroutine(_selfCheckCoroutine);
    }

    private void GameEventsOnGameOver()
    {
        DeSpawn();
    }

    private void GameEventsOnPlayerHitByAsteroid(Collider asteroidCollider)
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
            _containerMoveTransform.position = GameUtils.FindTeleportPlace(_containerMoveTransform, borderType);
        }
    }
    
    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Projectile"))
        {
            GameEvents.FireAsteroidHitByProjectile(otherCollider, _collider, this, _containerMoveTransform); 
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
            _containerMoveTransform.Translate(new Vector3(randX,randY , 0) * (_speed * Time.deltaTime));
        }    
    }

    private IEnumerator SelfDestructIfTooFar()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (Vector3.Distance(_containerMoveTransform.position, Vector3.zero) >= Constants.SELF_DESTRUCT_DISTANCE)
            {
                GameEvents.FireAsteroidSelfDestructed(_collider, this, _containerMoveTransform);
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
        Debug.LogFormat("Despawning {0}", _containerMoveTransform.gameObject.name);
        ObjectPooler.Instance.RecyclePooledObject(_containerMoveTransform.gameObject);
    }
    
}
