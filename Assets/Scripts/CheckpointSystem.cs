using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    [SerializeField] private PositionManager _posMng;

    private void OnTriggerEnter(Collider coll)
    {
        _posMng.SetNewValues(transform.position, transform.rotation.eulerAngles);
    }
}
