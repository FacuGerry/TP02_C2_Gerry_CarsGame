using System;
using System.Collections;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event Action OnPlayerDamaged;
    public event Action<int, int> OnLifeUpdated; // current life, max life
    public event Action OnPlayerDie;

    [SerializeField] private CarSettingsSO _data;
    private CollisionController _collisionController;
    private float _durability;

    private IEnumerator _coroutineHeal;

    private void Awake()
    {
        _collisionController = GetComponent<CollisionController>();
    }

    private void Start()
    {
        _durability = _data.maxDurability;
        Debug.Log("car has " + _durability + " left");
        OnLifeUpdated?.Invoke((int)_durability, (int)_data.maxDurability);
    }

    private void OnEnable()
    {
        _collisionController.OnPlayerCrashed += GetDamaged;
    }

    private void OnDisable()
    {
        _collisionController.OnPlayerCrashed -= GetDamaged;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator Healing()
    {
        while (_durability < _data.maxDurability)
        {
            _durability += _data.durabilityToCharge * Time.deltaTime;

            if (_durability >= _data.maxDurability)
                _durability = _data.maxDurability;

            OnLifeUpdated?.Invoke((int)_durability, (int)_data.maxDurability);

            yield return null;
        }
        yield return null;
    }

    private void GetDamaged(int damage)
    {
        _durability -= damage;
        if (_durability < 0)
        {
            _durability = 0;
            OnPlayerDie?.Invoke();
        }
        Debug.Log("car has " + _durability + " left");
        OnPlayerDamaged?.Invoke();
        OnLifeUpdated?.Invoke((int)_durability, (int)_data.maxDurability);
    }

    public void TakeDamage(int dmg)
    {
        GetDamaged(dmg);
    }

    public void Heal()
    {
        if (_coroutineHeal != null)
            StopCoroutine(_coroutineHeal);

        _coroutineHeal = Healing();
        StartCoroutine(_coroutineHeal);
    }

    public void StopHeal()
    {
        if (_coroutineHeal != null)
            StopCoroutine(_coroutineHeal);
    }
}