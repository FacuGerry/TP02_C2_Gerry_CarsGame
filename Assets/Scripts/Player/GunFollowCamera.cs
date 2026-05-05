using UnityEngine;

public class GunFollowCamera : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float _yOffset = 180f;

    private void Update()
    {
        Vector3 rotation = _camera.eulerAngles;
        rotation.x *= -1;
        rotation.y += _yOffset;
        transform.eulerAngles = rotation;
    }
}
