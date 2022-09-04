using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireControl : MonoBehaviour
{
    [SerializeField] private GameObject BulletObject;
    [Range(0.1f, 1f)][SerializeField] private float _periodBetweenTwoShots;
    [Range(1f, 10f)][SerializeField] private float _projectileSpeed;
    private bool _canShoot = true;

    private void Start()
    {
        StartCoroutine("ShootWaitCoroutine");
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // GameObject go = ObjectPooler.Instance.GetPooledObject(BulletObject);
            // go.transform.position = transform.position;
            // go.transform.parent = transform.parent;
            // go.SetActive(true);
            FireProjectile();
        }
    }

    private void FireProjectile()
    {
        if (!_canShoot)
            return;
        GameEvents.FireProjectileFired(transform, _projectileSpeed );
        _canShoot = false;
        StartCoroutine(ShootWaitCoroutine());
    }

    private IEnumerator ShootWaitCoroutine()
    {
        {
            yield return new WaitForSeconds(_periodBetweenTwoShots);
           _canShoot = true;
        }
        
    }
}
