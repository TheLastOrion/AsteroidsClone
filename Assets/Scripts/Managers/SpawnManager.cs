using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;
    
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
        projectile.SetActive(true);
        projectileControl.SetMovement(playerTransform.up);
        // projectileControl.SetMovement();
        
    }
    
}
