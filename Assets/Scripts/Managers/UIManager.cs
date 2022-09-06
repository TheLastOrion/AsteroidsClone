using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private TextMeshProUGUI ScoreText;
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
        UIEvents.HighScoresButtonPressed += UIEventsOnHighScoresButtonPressed;
        UIEvents.PlayGameButtonPressed += UIEventsOnPlayGameButtonPressed;
        SceneManager.sceneLoaded += SceneManagerOnsceneLoaded;
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
