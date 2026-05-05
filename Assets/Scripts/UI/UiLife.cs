using TMPro;
using UnityEngine;

public class UiLife : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _life;
    [SerializeField] private SelectionsSO _selection;
    private HealthSystem _healthSystem;

    private void Awake()
    {
        _healthSystem = _selection.spawnedCar.GetComponent<HealthSystem>();
    }

    private void OnEnable()
    {
        _healthSystem.OnLifeUpdated += UpdateLife;
    }

    private void OnDisable()
    {
        _healthSystem.OnLifeUpdated -= UpdateLife;
    }

    private void UpdateLife(int life, int maxLife)
    {
        _life.text = life.ToString();
    }
}
