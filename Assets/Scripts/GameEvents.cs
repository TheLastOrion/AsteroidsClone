using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static event Action ProjectileFired;
    public static event Action AsteroidHitByProjectile;

    public static void FireProjectileFired()
    {
        if (ProjectileFired != null)
            ProjectileFired();
    }
    
    public static void FireAsteroidHitByProjectile()
    {
        if (ProjectileFired != null)
            ProjectileFired();
    }
    
    
}
