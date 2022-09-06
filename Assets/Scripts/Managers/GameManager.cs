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
        GameEvents.GameOver += GameEventsOnGameOver;
    }

    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            GameEvents.FireGameOver();
        }

        if (Input.GetKeyDown(KeyCode.Insert))
        {
            GameEvents.FireGameStarted();
        }
    }
    
    private void GameEventsOnGameOver()
    {
        _score = 0;
        GameEvents.FireScoreChanged(_score);
    }
    
    private void GameEventsOnAsteroidHitByProjectile(Collider projectileCollider, Collider asteroidCollider, AsteroidControl asteroidControl)
    {
        //TODO remove this switch case and introduce Automatic enum dictionary calculations on inspector (probably will require serializable dictionary
        switch (asteroidControl.GetAsteroidSize())
        {
            case AsteroidSize.Small:
                _score += SmallAsteroidScore;
                break;
            case AsteroidSize.Medium:
                _score += MediumAsteroidScore;
                break;
            case AsteroidSize.Large:
                _score += LargeAsteroidScore;
                break;
        }
        GameEvents.FireScoreChanged(_score);
        if (_score > _highScore)
        {
            _highScore += _score;
            GameEvents.FireHighScoreChanged(_highScore);
            
        }
    }
}
