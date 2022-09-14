using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;
    [SerializeField] private List<GameObject> SmallAsteroidTypes = new List<GameObject>();
    [SerializeField] private List<GameObject> MediumAsteroidTypes = new List<GameObject>();
    [SerializeField] private List<GameObject> LargeAsteroidTypes = new List<GameObject>();
    [SerializeField] private int MinNumberOfAsteroidChunks;
    [SerializeField] private int MaxNumberOfAsteroidChunks;
    [SerializeField] private float TimeBetweenWaves;
    [SerializeField] private float TimeBetweenAsteroids;
    [SerializeField] private int MaxAsteroidPerWave;
    [SerializeField] private int MinAsteroidPerWave;
    [SerializeField] private float SpawnDistanceFromFighter;
    
    [SerializeField] private float DifficultyTimeBetweenWavesDegradation;

    [SerializeField] private GameObject FigterObject;
    [SerializeField] private GameObject Projectile;
    private Dictionary<int, AsteroidControl> CurrentAsteroids = new Dictionary<int, AsteroidControl>();
    private Coroutine _startWavesCoroutine;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        GameEvents.GameStarted += GameEventsOnGameStarted;
        GameEvents.GameOver += GameEventsOnGameOver;
        GameEvents.ProjectileFired += GameEventsOnProjectileFired;
        GameEvents.AsteroidHitByProjectile += GameEventsOnAsteroidHitByProjectile;
    }



    private void GameEventsOnGameStarted()
    {
        _startWavesCoroutine = StartCoroutine(SpawnAsteroidWavesCoroutine(TimeBetweenWaves, TimeBetweenAsteroids, MinAsteroidPerWave, MaxAsteroidPerWave));
    }
    
    private void GameEventsOnGameOver()
    {
        StopCoroutine(_startWavesCoroutine);
    }
    
    private void GameEventsOnAsteroidHitByProjectile(Collider projectileCollider, Collider asteroidCollider, AsteroidControl asteroidControl, Transform asteroidContainerTransform)
    {
        AsteroidSize size = asteroidControl.GetAsteroidSize();
        if (size == AsteroidSize.Large)
        {

            int amount = Random.Range(MinNumberOfAsteroidChunks, MaxNumberOfAsteroidChunks + 1);
            Debug.LogFormat("{0} size asteroid destroyed, spawning {1} mid asteroids",size, amount);

            for (int i = 0; i < amount; i++)
            {
                SpawnAsteroid(AsteroidSize.Medium, asteroidContainerTransform.position);
            }
        }
        else if (size == AsteroidSize.Medium)
        {
            int amount = Random.Range(MinNumberOfAsteroidChunks, MaxNumberOfAsteroidChunks + 1);
            Debug.LogFormat("{0} size asteroid destroyed, spawning {1} small asteroids",size, amount);
            for (int i = 0; i < amount; i++)
            {
                SpawnAsteroid(AsteroidSize.Small, asteroidContainerTransform.position);
            }
        }

    }

    private void GameEventsOnProjectileFired(Transform playerTransform, float projectileSpeed)
    {
        GameObject projectile = ObjectPooler.Instance.GetPooledObject(Projectile);
        ProjectileControl projectileControl = projectile.GetComponent<ProjectileControl>();
        projectile.transform.position = playerTransform.position;
        projectile.transform.SetParent(this.transform);
        projectile.SetActive(true);
        projectileControl.SetMovement(playerTransform.up);
        
    }

    private IEnumerator SpawnAsteroidWavesCoroutine(float timeBetweenWaves, float timeBetweenAsteroids, int minAsteroidPerWave, int maxAsteroidPerWave)
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenWaves);
            int asteroidCount = Random.Range(minAsteroidPerWave, maxAsteroidPerWave + 1);
            for (int i = 0; i < asteroidCount; i++)
            {
                SpawnAsteroid();
                yield return new WaitForSeconds(timeBetweenAsteroids);
            }
        }
    }

    private void SpawnAsteroid()
    {
        SpawnAsteroid((AsteroidSize)(Random.Range(0, Enum.GetValues(typeof(AsteroidSize)).Length)));
    }

    private void SpawnAsteroid(AsteroidSize size)
    {
        SpawnAsteroid(size, null);
    }

    private void SpawnAsteroid(AsteroidSize size, Vector3? position)
    {
        int index;
        GameObject asteroid = null;
        switch (size)
        {
            case AsteroidSize.Small:
                index = Random.Range(0, SmallAsteroidTypes.Count);
                asteroid = ObjectPooler.Instance.GetPooledObject(SmallAsteroidTypes[index]);
                Debug.LogFormat("GetPooledObject Name {0}", asteroid.name);
                break;
            case AsteroidSize.Medium:
                index = Random.Range(0, MediumAsteroidTypes.Count);
                asteroid = ObjectPooler.Instance.GetPooledObject(MediumAsteroidTypes[index]);
                Debug.LogFormat("GetPooledObject Name {0}", asteroid.name);

                break;
            case AsteroidSize.Large:
                index = Random.Range(0, LargeAsteroidTypes.Count);
                asteroid = ObjectPooler.Instance.GetPooledObject(LargeAsteroidTypes[index]);
                Debug.LogFormat("GetPooledObject Name {0}", asteroid.name);

                break;
            default:
                Debug.LogErrorFormat("Asteroid Type not found, likely a bug!");
                break;
        }

        AsteroidControl asteroidControl = asteroid.GetComponentInChildren<AsteroidControl>();
        if (position != null)
        {
            asteroid.transform.position = (Vector3)position;
        }
        else
        {
            asteroid.transform.position = GameUtils.FindProperRandomPositionAwayFromFighter(SpawnDistanceFromFighter,
                FigterObject.transform.position);
        }
        
        asteroid.transform.SetParent(transform);
        asteroidControl.SetRandomSpeed();
        asteroid.SetActive(true);
        asteroidControl.SetMovement(GameUtils.GetRandomizeDirectionVector());
    }
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SpawnAsteroid((AsteroidSize)Random.Range(0, Enum.GetValues(typeof(AsteroidSize)).Length), null);
        }

        
    }
}
