using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CarSettingsSO _data;
    [SerializeField] private Transform _firstPerson;
    [SerializeField] private Transform _thirdPerson;
    [SerializeField] private Transform _car;

    private bool _isFirstPerson = false;

    private void Start()
    {
        FollowCar(_isFirstPerson);
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
            transform.LookAt(_car);
        }
    }
}
