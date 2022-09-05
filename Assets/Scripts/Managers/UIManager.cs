
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
    }
}
