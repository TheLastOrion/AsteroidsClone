using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private TextMeshProUGUI LivesText;
    [SerializeField] private GameObject GamePanel;
    [SerializeField] private GameObject HighScorePanel;
    [SerializeField] private GameObject SelectPanel;
    private bool _mainSceneLoaded = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        GameEvents.ScoreChanged += GameEventsOnScoreChanged; 
        GameEvents.GameOver += GameEventsOnGameOver;
        GameEvents.LifeLost += GameEvents_LifeLost;
        GameEvents.GameStarted += GameEvents_GameStarted;
        UIEvents.HighScoresButtonPressed += UIEventsOnHighScoresButtonPressed;
        UIEvents.PlayGameButtonPressed += UIEventsOnPlayGameButtonPressed;
        SceneManager.sceneLoaded += SceneManagerOnsceneLoaded;
    }

    private void GameEvents_GameStarted()
    {
        LivesText.text = "Lives: " + Constants.PLAYER_LIVES;
    }

    private void GameEvents_LifeLost(int remainingLives)
    {
        LivesText.text = "Lives: " + remainingLives;
    }

    private void SceneManagerOnsceneLoaded(Scene scene, LoadSceneMode loadMode)
    {
        if (scene.name == "MainScene" && scene.isLoaded)
        {
            Debug.LogError("GAME START1");
            GameEvents.FireGameStarted();
        }
    }
    private void GameEventsOnGameOver()
    {
        GamePanel.SetActive(false);
        HighScorePanel.SetActive(false);
        SelectPanel.SetActive(true);
    }

    private void UIEventsOnPlayGameButtonPressed()
    {
        GamePanel.SetActive(true);
        HighScorePanel.SetActive(false);
        SelectPanel.SetActive(false);
        if (!_mainSceneLoaded)
        {
            SceneManager.LoadScene("MainScene", LoadSceneMode.Additive);
            Debug.LogFormat("Loading Scene additively");
            _mainSceneLoaded = true;
        }
        else
        {
            Debug.LogError("GAME START2");

            GameEvents.FireGameStarted();
        }
    }

    private void UIEventsOnHighScoresButtonPressed()
    {
        GamePanel.SetActive(false);
        HighScorePanel.SetActive(true);
        SelectPanel.SetActive(false);
    }

    private void GameEventsOnScoreChanged(int newScore)
    {
        ScoreText.text = "Score: " + newScore;
    }
}
