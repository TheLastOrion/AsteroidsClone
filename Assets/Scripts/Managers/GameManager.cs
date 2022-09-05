
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject Fighter;
    [SerializeField] private int SmallAsteroidScore;
    [SerializeField] private int MediumAsteroidScore;
    [SerializeField] private int LargeAsteroidScore;
    private int _score;
    private int _highScore;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        GameEvents.AsteroidHitByProjectile += GameEventsOnAsteroidHitByProjectile;
        
    }

    private void GameEventsOnAsteroidHitByProjectile(Collider projectileCollider, Collider asteroidCollider, AsteroidSize size)
    {
        // _score += asteroidCollider.gameObject.GetComponent<>;
        // GameEvents.FireScoreChanged(_score);
        // if (_score > _highScore)
        // {
        //     _highScore += 
        //     GameEvents.FireHighScoreChanged(_highScore);
        //     
        // }
    }
}
