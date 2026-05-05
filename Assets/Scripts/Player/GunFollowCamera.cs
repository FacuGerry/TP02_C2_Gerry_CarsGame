using UnityEngine;

public class GunFollowCamera : MonoBehaviour
{
    [SerializeField] private float _yOffset = 180f;
    private Transform _camera;

    private void Awake()
    {
        _camera = Camera.main.transform;
    }

    private void Update()
    {
        Vector3 rotation = _camera.eulerAngles;
        rotation.x *= -1;
        rotation.y += _yOffset;
        transform.eulerAngles = rotation;
    }
}
