using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action GameStarted;
    public static event Action GameOver;
    public static event Action<Transform, float> ProjectileFired;
    public static event Action<Collider, Collider, AsteroidControl, Transform> AsteroidHitByProjectile;
    public static event Action<BorderType, Collider> BorderExit;

    public static event Action<Collider> PlayerHitByAsteroid;
    public static event Action<Collider, AsteroidControl, Transform> AsteroidSelfDestructed;
    public static event Action<int> LifeLost;
    public static event Action<int> ScoreChanged;
    public static event Action<int> HighScoreChanged;

    public static void FireProjectileFired(Transform playerTransform, float projectileSpeed)
    {
        ProjectileFired?.Invoke(playerTransform, projectileSpeed);
    }
    
    public static void FireAsteroidHitByProjectile(Collider projectileCollider, Collider asteroidCollider, AsteroidControl asteroidControl, Transform asteroidContainerTransform)
    {
        AsteroidHitByProjectile?.Invoke(projectileCollider, asteroidCollider, asteroidControl, asteroidContainerTransform);
    }

    public static void FireBorderExit(BorderType borderType, Collider collider)
    {
        BorderExit?.Invoke(borderType, collider);
    }

    public static void FirePlayerHitByAsteroid(Collider asteroidCollider)
    {
        PlayerHitByAsteroid?.Invoke(asteroidCollider);
    }

    public static void FireAsteroidSelfDestructed(Collider asteroidCollider, AsteroidControl asteroidControl, Transform asteroidContainerTransform)
    {
        AsteroidSelfDestructed?.Invoke(asteroidCollider, asteroidControl, asteroidContainerTransform);
    }

    public static void FireLifeLost(int remainingLives)
    {
        LifeLost?.Invoke(remainingLives);
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
        Debug.LogErrorFormat("GAME OVER");
        GameOver?.Invoke();
    }
    
}
