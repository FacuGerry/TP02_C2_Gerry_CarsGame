using UnityEngine;

public class MineController : MonoBehaviour
{
    [SerializeField] private ObjectDataSO _data;
    [SerializeField] private LayerMask _targetLayer;

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 5f, Color.red);

        RaycastHit ray;
        if (Physics.Raycast(transform.position, transform.up, out ray, 5f, _targetLayer))
        {
            if (ray.collider != null)
            {
                HealthSystem health = ray.collider.GetComponentInParent<HealthSystem>();
                if (health != null)
                {
                    health.TakeDamage(_data.damage);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}