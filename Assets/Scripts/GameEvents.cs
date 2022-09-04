using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static event Action<Transform, float> ProjectileFired;
    public static event Action<Collider, Collider> AsteroidHitByProjectile;
    public static event Action<BorderType, Collider> BorderExit;

    public static event Action<Collider> PlayerHitByAsteroid;

    public static void FireProjectileFired(Transform playerTransfom, float projectileSpeed)
    {
        ProjectileFired?.Invoke(playerTransfom, projectileSpeed);
    }
    
    public static void FireAsteroidHitByProjectile(Collider projectileCollider, Collider asteroidCollider)
    {
        AsteroidHitByProjectile?.Invoke(projectileCollider, asteroidCollider);
    }

    public static void FireBorderExit(BorderType borderType, Collider collider)
    {
        BorderExit?.Invoke(borderType, collider);
    }

    public static void FirePlayerHitByAsteroid(Collider asteroidCollider)
    {
        PlayerHitByAsteroid?.Invoke(asteroidCollider);
    }
    
    
}
