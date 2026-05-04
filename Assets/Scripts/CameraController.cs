using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private KeyBindingsSO _keys;
    [SerializeField] private Transform _firstPerson;
    [SerializeField] private Transform _thirdPerson;
    [SerializeField] private Transform _car;

    private float _yaw;
    private float _pitch;

    private bool _isFirstPerson = false;

    private void Start()
    {
        FollowCar();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_keys.changePOV))
            _isFirstPerson = !_isFirstPerson;

        FollowCar();
        LookAround();
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

        float mouseY = -Input.GetAxis("Mouse Y");

        _pitch += mouseY;

        transform.rotation = Quaternion.Euler(_car.eulerAngles.x + _pitch, _car.eulerAngles.y + _yaw, 0f);
    }
}
