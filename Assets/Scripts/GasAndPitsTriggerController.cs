using UnityEngine;

public class GasAndPitsTriggerController : MonoBehaviour
{
    [SerializeField] private bool _isPitStop = false;

    private Collider _coll;
    private bool _isInside = false;

    private void Awake()
    {
        _coll = GetComponent<Collider>();
    }

    private void OnTriggerStay(Collider coll)
    {
        _isInside = IsFullyInside(coll);
        if (_isInside)
        {
            if (_isPitStop)
            {
                HealthSystem carHealth = coll.GetComponentInParent<HealthSystem>();
                if (carHealth != null)
                    carHealth.Heal();
            }
            else
            {
                GasSystem carGas = coll.GetComponentInParent<GasSystem>();
                if (carGas != null)
                    carGas.FuelUp();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _isInside = false;
    }

    private bool IsFullyInside(Collider car)
    {
        Bounds bounds = car.bounds;

        Vector3 min = bounds.min;
        Vector3 max = bounds.max;

        Vector3[] corners =
        {
            new Vector3(min.x, min.y, min.z),
            new Vector3(max.x, min.y, min.z),
            new Vector3(min.x, max.y, min.z),
            new Vector3(max.x, max.y, min.z),
            new Vector3(min.x, min.y, max.z),
            new Vector3(max.x, min.y, max.z),
            new Vector3(min.x, max.y, max.z),
            new Vector3(max.x, max.y, max.z)
        };

        foreach (Vector3 corner in corners)
        {
            Vector3 closest = _coll.ClosestPoint(corner);

            if (closest != corner)
                return false;
        }

        return true;
    }
}
