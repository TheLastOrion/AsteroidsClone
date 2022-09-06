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
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        GameEvents.ScoreChanged += GameEventsOnScoreChanged; 
        UIEvents.HighScoresButtonPressed += UIEventsOnHighScoresButtonPressed;
        UIEvents.PlayGameButtonPressed += UIEventsOnPlayGameButtonPressed;
    }

    private void UIEventsOnPlayGameButtonPressed()
    {
        GamePanel.SetActive(true);
        HighScorePanel.SetActive(false);
        SelectPanel.SetActive(false);
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("MainScene"))
        {
            SceneManager.LoadScene("MainScene", LoadSceneMode.Additive);
            Debug.LogFormat("Loading Scene additively");
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
