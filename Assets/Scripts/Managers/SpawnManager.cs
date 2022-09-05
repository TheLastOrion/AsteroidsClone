﻿using System;
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
    [SerializeField] private GameObject Projectile;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        GameEvents.ProjectileFired += GameEventsOnProjectileFired;
        GameEvents.AsteroidHitByProjectile += GameEventsOnAsteroidHitByProjectile;
    }

    private void GameEventsOnAsteroidHitByProjectile(Collider projectileCollider, Collider asteroidCollider)
    {
        AsteroidControl destroyedAsteroidControl = asteroidCollider.gameObject.GetComponent<AsteroidControl>();
        
        if (destroyedAsteroidControl.GetAsteroidSize() == AsteroidSize.Large)
        {

            int amount = Random.Range(MinNumberOfAsteroidChunks, MaxNumberOfAsteroidChunks + 1);
            Debug.LogFormat("{0} size asteroid destroyed, spawning {1} mid asteroids",destroyedAsteroidControl.GetAsteroidSize(), amount);

            for (int i = 0; i < amount; i++)
            {
                SpawnAsteroid(AsteroidSize.Medium, asteroidCollider.gameObject.transform.position);
            }
        }
        else if (destroyedAsteroidControl.GetAsteroidSize() == AsteroidSize.Medium)
        {
            int amount = Random.Range(MinNumberOfAsteroidChunks, MaxNumberOfAsteroidChunks + 1);
            Debug.LogFormat("{0} size asteroid destroyed, spawning {1} small asteroids",destroyedAsteroidControl.GetAsteroidSize(), amount);
            for (int i = 0; i < amount; i++)
            {
                SpawnAsteroid(AsteroidSize.Small, asteroidCollider.gameObject.transform.position);
            }
        }
        // if (destroyedAsteroidControl.GetAsteroidSize() != AsteroidSize.Small)
        // {
        //     int amount = Random.Range(MinNumberOfAsteroidChunks, MaxNumberOfAsteroidChunks + 1);
        //     for (int i = 0; i < amount; i++)
        //     {
        //         SpawnAsteroid(destroyedAsteroidControl.GetAsteroidSize() - 1, asteroidCollider.gameObject.transform.position);
        //     }
        // }
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

    private void SpawnAsteroid(AsteroidSize size, Vector3? position)
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

        AsteroidControl asteroidControl = asteroid.GetComponentInChildren<AsteroidControl>();
        if (position != null)
        {
            asteroid.transform.position = (Vector3)position;
        }
        else
        {
            asteroid.transform.position = new Vector3(
                Random.Range(
                    Constants.SPAWN_MIN_COORD_X, 
                    Constants.SPAWN_MAX_COORD_X), 
                Random.Range(
                    Constants.SPAWN_MIN_COORD_Y, 
                    Constants.SPAWN_MAX_COORD_Y));
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
