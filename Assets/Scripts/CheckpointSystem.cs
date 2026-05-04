using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    private void OnTriggerEnter(Collider coll)
    {
        PositionManager.Instance.SetNewValues(transform.position, transform.rotation.eulerAngles);
    }
}
