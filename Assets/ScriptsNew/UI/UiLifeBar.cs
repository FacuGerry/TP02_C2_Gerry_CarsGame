using UnityEngine;
using UnityEngine.UI;

public class UiLifeBar : MonoBehaviour
{
    [Header("Events subscribers")]
    [SerializeField] private HealthSystem _healthSystem;

    [SerializeField] private Image _barLife;

    private void OnEnable()
    {
        _healthSystem.OnLifeUpdated += OnUpdateLife_UpdateLifeBar;
    }

    private void OnDisable()
    {
        _healthSystem.OnLifeUpdated -= OnUpdateLife_UpdateLifeBar;
    }

    private void OnUpdateLife_UpdateLifeBar(int life, int maxLife)
    {
        float lerp = life / (float)maxLife;
        if (lerp <= 0)
            _barLife.fillAmount = 0;
        else
            _barLife.fillAmount = lerp;
    }
}
