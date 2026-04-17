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
        _brakeForce = Input.GetAxis("Brake") * _data.brakePower;
        _steeringForce = Input.GetAxis("Horizontal") * _data.steeringPower;
    }

    private void FixedUpdate()
    {
        _wheelFR.motorTorque = _accelerationForce;
        _wheelFL.motorTorque = _accelerationForce;
        _wheelRR.motorTorque = _accelerationForce;
        _wheelRL.motorTorque = _accelerationForce;

        _wheelFR.brakeTorque = _brakeForce;
        _wheelFL.brakeTorque = _brakeForce;
        _wheelRR.brakeTorque = _brakeForce;
        _wheelRL.brakeTorque = _brakeForce;

        _wheelFR.steerAngle = _steeringForce;
        _wheelFL.steerAngle = _steeringForce;

        SyncVisuals(_wheelFR, _wheelVisualFR);
        SyncVisuals(_wheelFL, _wheelVisualFL);
        SyncVisuals(_wheelRR, _wheelVisualRR);
        SyncVisuals(_wheelRL, _wheelVisualRL);
    }

    private void SyncVisuals(WheelCollider coll, Transform visual)
    {
        coll.GetWorldPose(out Vector3 pos, out Quaternion rot);
        visual.position = pos;
        visual.rotation = rot;
    }
}
