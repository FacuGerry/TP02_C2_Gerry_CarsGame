using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CarSettingsSO _data;
    [SerializeField] private Transform _firstPerson;
    [SerializeField] private Transform _thirdPerson;
    [SerializeField] private float _angleFirstPerson = 0f;
    [SerializeField] private float _angleThirdPerson = 15f;
    [SerializeField] private Transform _car;

    private float _yaw;
    private float _pitch;

    private bool _isFirstPerson = false;

    private void Start()
    {
        SetRotation();
        FollowCar();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_data.changePOV))
        {
            _isFirstPerson = !_isFirstPerson;
            SetRotation();
        }

        FollowCar();
        LookAround();
    }

    private void SetRotation()
    {
        _pitch = _isFirstPerson ? _angleFirstPerson : _angleThirdPerson;
    }

    private void FollowCar()
    {
        Vector3 positionToGo = _isFirstPerson ?
            new Vector3(_firstPerson.position.x, _firstPerson.position.y, _firstPerson.position.z) :
            new Vector3(_thirdPerson.position.x, _thirdPerson.position.y, _thirdPerson.position.z);
        transform.position = positionToGo;
    }

    private void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X");

        _yaw += mouseX;

        transform.rotation = Quaternion.Euler(_pitch, _car.eulerAngles.y + _yaw, 0f);
    }
}
