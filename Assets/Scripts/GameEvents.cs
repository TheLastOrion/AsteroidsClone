using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static event Action ProjectileFired;
    public static event Action<Collider, Collider> AsteroidHitByProjectile;
    public static event Action<BorderType, Collider> BorderExit;

    public static void FireProjectileFired()
    {
        if (ProjectileFired != null)
            ProjectileFired();
    }
    
    public static void FireAsteroidHitByProjectile(Collider projectileCollider, Collider asteroidCollider)
    {
        if (AsteroidHitByProjectile != null)
            FireAsteroidHitByProjectile(projectileCollider, asteroidCollider);
    }

    public static void FireBorderExit(BorderType borderType, Collider collider)
    {
        if (BorderExit != null)
            BorderExit(borderType, collider);
    }
    
    
}
