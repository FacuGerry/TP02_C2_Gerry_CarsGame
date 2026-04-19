using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CarSettingsSO _data;
    [SerializeField] private Transform _firstPerson;
    [SerializeField] private Transform _thirdPerson;
    [SerializeField] private Transform _car;

    private float _yaw;

    private bool _isFirstPerson = false;

    private void Start()
    {
        FollowCar(_isFirstPerson);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_data.changePOV))
            _isFirstPerson = !_isFirstPerson;

        FollowCar(_isFirstPerson);
    }

    private void FollowCar(bool isFirstPerson)
    {
        if (isFirstPerson)
        {
            transform.position = _firstPerson.position;
            transform.rotation = _firstPerson.localRotation;
        }
        else
        {
            transform.position = _thirdPerson.position;
            LookAround();
        }
    }

    private void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X");

        _yaw += mouseX;

        Quaternion rotation = Quaternion.AngleAxis(_yaw, Vector3.up) * Quaternion.LookRotation(_thirdPerson.forward, Vector3.up);

        transform.rotation = rotation;
    }
}
