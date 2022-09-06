using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action GameStarted;
    public static event Action GameOver;
    public static event Action<Transform, float> ProjectileFired;
    public static event Action<Collider, Collider, AsteroidControl> AsteroidHitByProjectile;
    public static event Action<BorderType, Collider> BorderExit;

    public static event Action<Collider, AsteroidControl> PlayerHitByAsteroid;
    public static event Action<Collider, AsteroidControl> AsteroidSelfDestructed;

    public static event Action<int> ScoreChanged;
    public static event Action<int> HighScoreChanged;

    public static void FireProjectileFired(Transform playerTransfom, float projectileSpeed)
    {
        ProjectileFired?.Invoke(playerTransfom, projectileSpeed);
    }
    
    public static void FireAsteroidHitByProjectile(Collider projectileCollider, Collider asteroidCollider, AsteroidControl asteroidControl)
    {
        AsteroidHitByProjectile?.Invoke(projectileCollider, asteroidCollider, asteroidControl);
    }

    public static void FireBorderExit(BorderType borderType, Collider collider)
    {
        BorderExit?.Invoke(borderType, collider);
    }

    public static void FirePlayerHitByAsteroid(Collider asteroidCollider, AsteroidControl asteroidControl)
    {
        PlayerHitByAsteroid?.Invoke(asteroidCollider, asteroidControl);
    }

    public static void FireAsteroidSelfDestructed(Collider asteroidCollider, AsteroidControl asteroidControl)
    {
        AsteroidSelfDestructed?.Invoke(asteroidCollider, asteroidControl);
    }

    public static void FireScoreChanged(int newScore)
    {
        ScoreChanged?.Invoke(newScore);
    }

    public static void FireHighScoreChanged(int newHighScore)
    {
        HighScoreChanged?.Invoke(newHighScore);
    }
    
    public static void FireGameStarted()
    {
        GameStarted?.Invoke();
    }
    public static void FireGameOver()
    {
        GameOver?.Invoke();
    }
    
}
