using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private void OnEnable()
    {
        Player.OnScoreUpdate += UpdateScore;
    }

    private void OnDisable()
    {
        Player.OnScoreUpdate -= UpdateScore;
    }
    private void UpdateScore(int score)
    {
        scoreText.text = "Score :" + score.ToString();
    }
}
