using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public static event Action<int> OnLifeUpdated;

    [SerializeField] private CarSettingsSO _data;
    private float _durability;

    private void Start()
    {
        _durability = _data.maxDurability;
        Debug.Log("car has " + _durability + " left");
        OnLifeUpdated?.Invoke((int)_durability);
    }

    private void OnEnable()
    {
        CollisionController.OnPlayerCrashed += GetDamaged;
    }

    private void OnDisable()
    {
        CollisionController.OnPlayerCrashed -= GetDamaged;
    }

    private void GetDamaged(int damage)
    {
        _durability -= damage;
        if (_durability < 0)
            _durability = 0;
        Debug.Log("car has " + _durability + " left");
        OnLifeUpdated?.Invoke((int)_durability);
    }

    public void Heal()
    {
        _durability = _data.maxDurability;
        Debug.Log("car healed and now has " + _durability + " left");
        OnLifeUpdated?.Invoke((int)_durability);
    }
}