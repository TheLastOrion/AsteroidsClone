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
    private bool _isHighScore = false;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        GameEvents.AsteroidHitByProjectile += GameEventsOnAsteroidHitByProjectile;
        GameEvents.GameStarted += GameEventsOnGameStarted;
        GameEvents.GameOver += GameEventsOnGameOver;
    }

    private void GameEventsOnGameStarted()
    {
        _isHighScore = false;
        _score = 0;
        GameEvents.FireScoreChanged(_score);
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
        if(_isHighScore)
            GameEvents.FireHighScoreChanged(_highScore);
    }

    private void GameEventsOnAsteroidHitByProjectile(Collider projectileCollider, Collider asteroidCollider, AsteroidControl asteroidControl, Transform asteroidContainerTransform)
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
            _isHighScore = true;
            _highScore += _score;
        }
    }
}
