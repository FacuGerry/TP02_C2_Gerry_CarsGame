using System;
using System.Collections.Generic;
using UnityEngine;

public class GasSystem : MonoBehaviour
{
    public static event Action<float> OnGasChange;

    [Header("Data")]
    [SerializeField] private CarSettingsSO _data;

    [Header("Wheels")]
    [SerializeField] private List<WheelCollider> _wheels = new List<WheelCollider>();

    private float _gas;

    private void Start()
    {
        _gas = _data.maxGas;
    }

    private void Update()
    {
        _gas -= _data.gasUsage * CalculateRPM() / 1000f;
        OnGasChange?.Invoke(_gas);
    }

    private float CalculateRPM()
    {
        float rpm = 0;
        foreach (WheelCollider wheel in _wheels)
            rpm += wheel.rpm;

        rpm /= _wheels.Count;

        return rpm;
    }
}
