using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    private HealthSystem _healthSystem;

    private void Awake()
    {
        _healthSystem = GetComponent<HealthSystem>();
    }

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
