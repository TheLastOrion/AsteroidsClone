using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

public class UIManager : MonoBehaviour
{
    //Since the game ui is simple, UIManager can be used to manage it all, however I know this is not clean.
    //TODO divide this class into many seperate instances, with UIManager residing on top.
    public static UIManager Instance;
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private TextMeshProUGUI LivesText;
    [SerializeField] private GameObject GamePanel;
    [SerializeField] private GameObject HighScorePanel;
    [SerializeField] private GameObject SelectPanel;
    [SerializeField] private Transform HighScoreScrollViewContainer;
    [SerializeField] private GameObject HighScoreObject;
    private Queue<int> HighScoreQueue = new Queue<int>(Constants.HIGH_SCORES_SHOWN_AMOUNT);
    private bool _mainSceneLoaded = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        GameEvents.ScoreChanged += GameEventsOnScoreChanged;
        GameEvents.HighScoreChanged += GameEvents_HighScoreChanged;
        GameEvents.GameOver += GameEventsOnGameOver;
        GameEvents.LifeLost += GameEvents_LifeLost;
        GameEvents.GameStarted += GameEvents_GameStarted;
        UIEvents.HighScoresButtonPressed += UIEventsOnHighScoresButtonPressed;
        UIEvents.PlayGameButtonPressed += UIEventsOnPlayGameButtonPressed;
        UIEvents.BackToMainMenuButtonPressed += UIEvents_BackToMainMenuButtonPressed;
        SceneManager.sceneLoaded += SceneManagerOnsceneLoaded;
    }

    private void GameEvents_HighScoreChanged(int highScore)
    {
        HighScoreQueue.Enqueue(highScore);
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
            Debug.Log("GAME START1");
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
            Debug.Log("GAME START2");
            GameEvents.FireGameStarted();
        }
    }

    private void UIEventsOnHighScoresButtonPressed()
    {
        GamePanel.SetActive(false);
        HighScorePanel.SetActive(true);
        SelectPanel.SetActive(false);
        UpdateHighScoreScrollView(HighScoreQueue);

    }
    private void UIEvents_BackToMainMenuButtonPressed()
    {
        GamePanel.SetActive(false);
        HighScorePanel.SetActive(false);
        SelectPanel.SetActive(true);
    }

    private void GameEventsOnScoreChanged(int newScore)
    {
        ScoreText.text = "Score: " + newScore;
    }

    private void UpdateHighScoreScrollView(Queue<int> highScores)
    {
        for (int i = highScores.Count; i >= 0; i--)
        {
            GameObject scoreObject = Instantiate(HighScoreObject, HighScoreScrollViewContainer);
            TextMeshProUGUI scoreText = scoreObject.GetComponent<TextMeshProUGUI>();
            int score = highScores.Dequeue();
            highScores.Enqueue(score);
            scoreText.text = score.ToString();
        }
    }
}
