using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;
    [SerializeField] private List<GameObject> SmallAsteroidTypes = new List<GameObject>();
    [SerializeField] private List<GameObject> MediumAsteroidTypes = new List<GameObject>();
    [SerializeField] private List<GameObject> LargeAsteroidTypes = new List<GameObject>();
    [SerializeField] private GameObject Projectile;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        GameEvents.ProjectileFired += GameEventsOnProjectileFired;
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

    private void SpawnAsteroid(AsteroidSize size)
    {
        int index;
        GameObject asteroid = null;
        switch (size)
        {
            case AsteroidSize.Small:
                index = Random.Range(0, SmallAsteroidTypes.Count-1);
                asteroid = ObjectPooler.Instance.GetPooledObject(SmallAsteroidTypes[index]);

                break;
            case AsteroidSize.Medium:
                index = Random.Range(0, MediumAsteroidTypes.Count-1);
                asteroid = ObjectPooler.Instance.GetPooledObject(MediumAsteroidTypes[index]);
                break;
            case AsteroidSize.Large:
                index = Random.Range(0, LargeAsteroidTypes.Count-1);
                asteroid = ObjectPooler.Instance.GetPooledObject(LargeAsteroidTypes[index]);
                break;
            default:
                Debug.LogErrorFormat("Asteroid Type not found, likely a bug!");
                break;
        }
        AsteroidControl asteroidControl = asteroid.GetComponent<AsteroidControl>();
        asteroid.transform.position = new Vector3(Random.Range(-7, 7), Random.Range(-3, 5));
        asteroid.transform.SetParent(this.transform);
        asteroidControl.SetRandomSpeed();
        asteroid.SetActive(true);
        asteroidControl.SetMovement(GameUtils.GetRandomizeDirectionVector());
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            int randomSize = Random.Range(0, 3);
            Debug.Log("Random Size: " +  randomSize);
            SpawnAsteroid((AsteroidSize)randomSize );
        }
    }
}
