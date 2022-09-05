using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<Transform, float> ProjectileFired;
    public static event Action<Collider, Collider, AsteroidSize> AsteroidHitByProjectile;
    public static event Action<BorderType, Collider> BorderExit;

    public static event Action<Collider> PlayerHitByAsteroid;
    public static event Action<Collider> AsteroidSelfDestructed;

    public static event Action<int> ScoreChanged;
    public static event Action<int> HighScoreChanged;

    public static void FireProjectileFired(Transform playerTransfom, float projectileSpeed)
    {
        ProjectileFired?.Invoke(playerTransfom, projectileSpeed);
    }
    
    public static void FireAsteroidHitByProjectile(Collider projectileCollider, Collider asteroidCollider, AsteroidSize size)
    {
        AsteroidHitByProjectile?.Invoke(projectileCollider, asteroidCollider, size);
    }

    public static void FireBorderExit(BorderType borderType, Collider collider)
    {
        BorderExit?.Invoke(borderType, collider);
    }

    public static void FirePlayerHitByAsteroid(Collider asteroidCollider)
    {
        PlayerHitByAsteroid?.Invoke(asteroidCollider);
    }

    public static void FireAsteroidSelfDestructed(Collider asteroidCollider)
    {
        AsteroidSelfDestructed?.Invoke(asteroidCollider);
    }

    public static void FireScoreChanged(int newScore)
    {
        ScoreChanged?.Invoke(newScore);
    }

    public static void FireHighScoreChanged(int newHighScore)
    {
        HighScoreChanged?.Invoke(newHighScore);
    }
    
}
