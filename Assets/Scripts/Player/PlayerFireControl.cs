using System.Collections;
using UnityEngine;

public class PlayerFireControl : MonoBehaviour
{
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
