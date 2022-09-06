using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private TextMeshProUGUI ScoreText;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        GameEvents.ScoreChanged += GameEventsOnScoreChanged; 
    }

    private void GameEventsOnScoreChanged(int newScore)
    {
        ScoreText.text = "Score: " + newScore;
    }
}
