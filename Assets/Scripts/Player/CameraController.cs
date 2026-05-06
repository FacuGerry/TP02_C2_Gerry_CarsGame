using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Keys")]
    [SerializeField] private KeyBindingsSO _keys;

    [Header("Target")]
    [SerializeField] private SelectionsSO _car;

    private Transform _firstPerson;            
    private Transform _thirdPerson;            
    private Transform _carTransform;

    private float _yaw;
    private float _pitch;

    private bool _isFirstPerson = false;

    private void Start()
    {
        _carTransform = _car.spawnedCar.transform;
        _firstPerson = _car.spawnedCar.GetComponentInChildren<FirstPerson>().GetGameObject().transform;
        _thirdPerson = _car.spawnedCar.GetComponentInChildren<ThirdPerson>().GetGameObject().transform;

        FollowCar();
    }

    private void Update()
    {
        if (!PauseGame.Instance.IsPaused && _carTransform.gameObject.activeInHierarchy)
        {
            if (Input.GetKeyDown(_keys.changePOV))
                _isFirstPerson = !_isFirstPerson;

            FollowCar();
            LookAround();
        }
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

        transform.rotation = Quaternion.Euler(_carTransform.eulerAngles.x + _pitch, _carTransform.eulerAngles.y + _yaw, 0f);
    }
}
