using System;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public event Action<float> OnSpeedChange;

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
    private GasSystem _gasSystem;

    private float _accelerationForce;
    private float _brakeForce;
    private float _steeringForce;
    private Rigidbody _rb;

    private bool _canMove = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _gasSystem = GetComponent<GasSystem>();
    }

    private void Start()
    {
        _rb.mass = _data.weight;
        _canMove = true;
    }

    private void OnEnable()
    {
        _gasSystem.OnGasEmpty += OnGasEmpty_StopMoving;
    }

    private void Update()
    {
        if (_canMove)
            _accelerationForce = Input.GetAxis("Vertical") * _data.horsePower;

        _brakeForce = Input.GetAxis("Brake") * _data.brakePower;
        _steeringForce = Input.GetAxis("Horizontal") * _data.steeringPower;

        if (Input.GetKeyDown(_data.restartCar))
            RestartCar();

        SyncVisuals(_wheelFR, _wheelVisualFR);
        SyncVisuals(_wheelFL, _wheelVisualFL);
        SyncVisuals(_wheelRR, _wheelVisualRR);
        SyncVisuals(_wheelRL, _wheelVisualRL);

        OnSpeedChange?.Invoke(_rb.linearVelocity.magnitude * 3.6f);
    }

    private void FixedUpdate()
    {
        Steer();
        Accelerate();
        Brake();
        FixRotation();
    }

    private void OnDisable()
    {
        _gasSystem.OnGasEmpty -= OnGasEmpty_StopMoving;
    }

    private void Accelerate()
    {
        if (_rb.linearVelocity.magnitude >= _data.maxSpeed)
            _accelerationForce = _data.maxSpeed;

        _wheelFR.motorTorque = _accelerationForce;
        _wheelFL.motorTorque = _accelerationForce;
        _wheelRR.motorTorque = _accelerationForce;
        _wheelRL.motorTorque = _accelerationForce;
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
        if (Mathf.Abs(_steeringForce) < 0.01f)
        {
            Quaternion currentRot = _rb.rotation;

            Quaternion targetRot = Quaternion.Euler(0f, currentRot.eulerAngles.y, 0f);

            _rb.rotation = Quaternion.Slerp(currentRot, targetRot, _data.timeToResetRotation * Time.fixedDeltaTime);
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

    private void OnGasEmpty_StopMoving()
    {
        _canMove = false;
        _accelerationForce = 0f;
    }
}
