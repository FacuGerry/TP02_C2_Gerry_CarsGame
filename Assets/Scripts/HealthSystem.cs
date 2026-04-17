using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private CarSettingsSO _data;
    private float _durability;
    private float _damage;

    private void Start()
    {
        _durability = _data.maxDurability;
    }

    void GetDamaged()
    {
        _durability -= _damage;
        if (_durability < 0 )
            _durability = 0;
    }
}