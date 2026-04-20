using UnityEngine;

public class GasAndPitsTriggerController : MonoBehaviour
{
    [SerializeField] private bool _isPitStop = false;

    private void OnTriggerEnter(Collider coll)
    {
        if (_isPitStop)
        {
            if (coll.TryGetComponent(out HealthSystem carHealth))
                carHealth.Heal();
        }
        else
            if (coll.TryGetComponent(out GasSystem carGas))
                carGas.FuelUp();
    }
}
