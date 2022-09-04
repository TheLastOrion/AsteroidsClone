using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    [SerializeField] private GameObject Projectile;
    public GameObject Fighter;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        GameEvents.ProjectileFired += GameEventsOnProjectileFired;
    }

    private void GameEventsOnProjectileFired(Vector3 playerPosition, Vector3 playerDirection, float projectileSpeed)
    {
        GameObject projectile = ObjectPooler.Instance.GetPooledObject(Projectile);
        ProjectileControl projectileControl = projectile.GetComponent<ProjectileControl>();
        projectile.transform.position = playerPosition;
        projectile.SetActive(true);
        // projectileControl.SetMovement();
        
    }

    void SpawnProjectile()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
