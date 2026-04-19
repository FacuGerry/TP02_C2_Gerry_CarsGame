using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private CarSettingsSO _data;
    private float _durability;

    private void Start()
    {
        _durability = _data.maxDurability;
        Debug.Log("car has " + _durability + " left");
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
        if (_durability < 0 )
            _durability = 0;
        Debug.Log("car has " + _durability + " left");
    }
}