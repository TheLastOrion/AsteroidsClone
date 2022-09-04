using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static event Action<Vector3, Vector3, float> ProjectileFired;
    public static event Action<Collider, Collider> AsteroidHitByProjectile;
    public static event Action<BorderType, Collider> BorderExit;

    public static void FireProjectileFired(Vector3 playerPosition, Vector3 playerDirection, float projectileSpeed)
    {
        if (ProjectileFired != null)
            ProjectileFired(playerPosition, playerDirection, projectileSpeed);
    }
    
    public static void FireAsteroidHitByProjectile(Collider projectileCollider, Collider asteroidCollider)
    {
        if (AsteroidHitByProjectile != null)
            AsteroidHitByProjectile(projectileCollider, asteroidCollider);
    }

    public static void FireBorderExit(BorderType borderType, Collider collider)
    {
        if (BorderExit != null)
            BorderExit(borderType, collider);
    }
    
    
}
