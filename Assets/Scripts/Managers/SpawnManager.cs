using System.Collections.Generic;
using UnityEngine;
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
                index = Random.Range(0, SmallAsteroidTypes.Count);
                asteroid = ObjectPooler.Instance.GetPooledObject(SmallAsteroidTypes[index]);

                break;
            case AsteroidSize.Medium:
                index = Random.Range(0, MediumAsteroidTypes.Count);
                asteroid = ObjectPooler.Instance.GetPooledObject(MediumAsteroidTypes[index]);
                break;
            case AsteroidSize.Large:
                index = Random.Range(0, LargeAsteroidTypes.Count);
                asteroid = ObjectPooler.Instance.GetPooledObject(LargeAsteroidTypes[index]);
                break;
            default:
                Debug.LogErrorFormat("Asteroid Type not found, likely a bug!");
                break;
        }

        asteroid = ObjectPooler.Instance.GetPooledObject(SmallAsteroidTypes[0]);
        AsteroidControl asteroidControl = asteroid.GetComponentInChildren<AsteroidControl>();
        asteroid.transform.position = new Vector3(
            Random.Range(
                Constants.SPAWN_MIN_COORD_X, 
                Constants.SPAWN_MAX_COORD_X), 
            Random.Range(
                Constants.SPAWN_MIN_COORD_Y, 
                Constants.SPAWN_MAX_COORD_Y));
        asteroid.transform.SetParent(transform);
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
            SpawnAsteroid(AsteroidSize.Small );
        }
    }
}
