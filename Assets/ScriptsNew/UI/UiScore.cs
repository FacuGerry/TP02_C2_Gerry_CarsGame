using TMPro;
using UnityEngine;

public class UiScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        ScoreController.OnScoreUpdated += OnScoreUpdated_UpdateScore;
    }

    private void OnDisable()
    {
        ScoreController.OnScoreUpdated -= OnScoreUpdated_UpdateScore;
    }

    private void OnScoreUpdated_UpdateScore(int score)
    {
        _text.text = "SCORE: " + score.ToString();
    }
}
