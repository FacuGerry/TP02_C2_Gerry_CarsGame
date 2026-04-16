using UnityEngine;

public class CarMovement : MonoBehaviour
{
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
    [SerializeField] private PlayerSettingsSO _data;

    private float _accelerationForce;
    private float _brakeForce;
    private float _steeringForce;

    private void Update()
    {
        _accelerationForce = Input.GetAxis("Vertical") * _data.horsePower;
        _brakeForce = Input.GetAxis("Jump") * _data.brakePower;
        _steeringForce = Input.GetAxis("Horizontal") * _data.steeringPower;
    }

    private void FixedUpdate()
    {
        _wheelFR.motorTorque = _accelerationForce;
        _wheelFL.motorTorque = _accelerationForce;
        _wheelRR.motorTorque = _accelerationForce;
        _wheelRL.motorTorque = _accelerationForce;

        _wheelFR.motorTorque = _brakeForce;
        _wheelFL.motorTorque = _brakeForce;
        _wheelRR.motorTorque = _brakeForce;
        _wheelRL.motorTorque = _brakeForce;
    }
}
