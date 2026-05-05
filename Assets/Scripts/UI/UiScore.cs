using TMPro;
using UnityEngine;

public class UiScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    private ScoreController _controller;

    private void Awake()
    {
        _controller = MyPoolManager.Instance.gameObject.GetComponent<ScoreController>();
    }

    private void OnEnable()
    {
        _controller.OnScoreUpdated += OnScoreUpdated_UpdateScore;
    }

    private void OnDisable()
    {
        _controller.OnScoreUpdated -= OnScoreUpdated_UpdateScore;
    }

    private void OnScoreUpdated_UpdateScore(int score)
    {
        _text.text = "SCORE: " + score.ToString();
    }
}
