using TMPro;
using UnityEngine;

public class UiLife : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _life;

    private void OnEnable()
    {
        HealthSystem.OnLifeUpdated += UpdateLife;
    }

    private void OnDisable()
    {
        HealthSystem.OnLifeUpdated -= UpdateLife;
    }

    private void UpdateLife(int life)
    {
        _life.text = life.ToString();
    }
}
