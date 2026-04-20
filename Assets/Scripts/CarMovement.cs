using System;
using System.Collections;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public static event Action<int> OnSpeedChange;

    [Header("Wheel Colliders")]
    [SerializeField] private WheelCollider _wheelFR;
    [SerializeField] private WheelCollider _wheelFL;
    [SerializeField] private WheelCollider _wheelRR;
    [SerializeField] private WheelCollider _wheelRL;

    [Header("Wheel Visuals")]
    [SerializeField] private Transform _wheelVisualFR;
    [SerializeField] private Transform _wheelVisualFL;
    [SerializeField] private Transform _wheelVisualRR;
    [SerializeField] private Transform _wheelVisualRL;

    [Header("Settings")]
    [SerializeField] private CarSettingsSO _data;
    [SerializeField] private float _timeToFixRotation = 0.3f;

    private float _accelerationForce;
    private float _brakeForce;
    private float _steeringForce;
    private Rigidbody _rb;

    private IEnumerator _coroutineFixing;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rb.mass = _data.weight;
    }

    private void Update()
    {
        _accelerationForce = Input.GetAxis("Vertical") * _data.horsePower;
        _brakeForce = Input.GetAxis("Brake") * _data.brakePower;
        _steeringForce = Input.GetAxis("Horizontal") * _data.steeringPower;

        if (Input.GetKeyDown(_data.restartCar))
            RestartCar();

        SyncVisuals(_wheelFR, _wheelVisualFR);
        SyncVisuals(_wheelFL, _wheelVisualFL);
        SyncVisuals(_wheelRR, _wheelVisualRR);
        SyncVisuals(_wheelRL, _wheelVisualRL);

        OnSpeedChange?.Invoke((int)_rb.linearVelocity.magnitude);

        FixRotation();
    }

    private void FixedUpdate()
    {
        Accelerate();
        Brake();
        Steer();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator FixingRotation()
    {
        float clock = 0f;
        Quaternion rotationToGo = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
        while (clock < _timeToFixRotation)
        {
            clock += Time.deltaTime;
            float lerp = clock / _timeToFixRotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, rotationToGo, lerp);
            yield return null;
        }
        yield return null;
    }

    private void Accelerate()
    {
        _wheelFR.motorTorque = _accelerationForce;
        _wheelFL.motorTorque = _accelerationForce;
        _wheelRR.motorTorque = _accelerationForce;
        _wheelRL.motorTorque = _accelerationForce;

        if (_wheelFR.motorTorque > _data.maxSpeed)
            _wheelFR.motorTorque = _data.maxSpeed;

        if (_wheelFL.motorTorque > _data.maxSpeed)
            _wheelFL.motorTorque = _data.maxSpeed;

        if (_wheelRR.motorTorque > _data.maxSpeed)
            _wheelRR.motorTorque = _data.maxSpeed;

        if (_wheelRL.motorTorque > _data.maxSpeed)
            _wheelRL.motorTorque = _data.maxSpeed;
    }

    private void Brake()
    {
        _wheelFR.brakeTorque = _brakeForce;
        _wheelFL.brakeTorque = _brakeForce;
        _wheelRR.brakeTorque = _brakeForce;
        _wheelRL.brakeTorque = _brakeForce;
    }

    private void Steer()
    {
        _wheelFR.steerAngle = _steeringForce;
        _wheelFL.steerAngle = _steeringForce;
    }

    private void FixRotation()
    {
        if (_steeringForce == 0f && transform.rotation.x != 0f)
        {
            if (_coroutineFixing != null)
                StopCoroutine(_coroutineFixing);

            _coroutineFixing = FixingRotation();
            StartCoroutine(_coroutineFixing);
        }
    }

    private void RestartCar()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);

        _rb.linearVelocity = Vector3.zero;

        _wheelFR.steerAngle = 0;
        _wheelFL.steerAngle = 0;
    }

    private void SyncVisuals(WheelCollider coll, Transform visual)
    {
        coll.GetWorldPose(out Vector3 pos, out Quaternion rot);
        visual.position = pos;
        visual.rotation = rot;
    }
}
