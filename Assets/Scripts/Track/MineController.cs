using UnityEngine;

public class MineController : MonoBehaviour, IPooleable
{
    [SerializeField] private ObjectDataSO _data;
    [SerializeField] private LayerMask _targetLayer;

    public bool IsActive { get; set; }

    private void Update()
    {
        RaycastHit ray;
        if (Physics.Raycast(transform.position, transform.up, out ray, 5f, _targetLayer))
        {
            if (ray.collider != null)
            {
                HealthSystem health = ray.collider.GetComponentInParent<HealthSystem>();
                if (health != null)
                {
                    health.TakeDamage(_data.damage);
                    DeActivate();
                }
            }
        }
    }

    public void Activate()
    {
        IsActive = true;
        gameObject.SetActive(IsActive);
    }

    public void DeActivate()
    {
        IsActive = false;
        gameObject.SetActive(IsActive);
    }
}