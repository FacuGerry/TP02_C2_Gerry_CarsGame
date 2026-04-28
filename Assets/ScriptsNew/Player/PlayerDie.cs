using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    [Header("Events subscribers")]
    [SerializeField] private HealthSystem _healthSystem;

    private void OnEnable()
    {
        _healthSystem.OnPlayerDie += OnPlayerDie_SetToInactive;
    }

    private void OnDisable()
    {
        _healthSystem.OnPlayerDie -= OnPlayerDie_SetToInactive;
    }

    private void OnPlayerDie_SetToInactive()
    {
        gameObject.SetActive(false);
    }
}
