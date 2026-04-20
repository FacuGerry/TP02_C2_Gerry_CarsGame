using System;
using System.Collections.Generic;
using UnityEngine;

public class GasSystem : MonoBehaviour
{
    public static event Action<float> OnGasChange;
    public event Action OnGasEmpty;

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
        // Por ahora anda, pero me gustaría tener una cuenta que gaste cada 'x' tiempo
        _gas -= _data.gasUsage * CalculateRPM() / 1000f;
        if (_gas < 0)
        {
            _gas = 0;
            OnGasEmpty?.Invoke();
        }

        OnGasChange?.Invoke(_gas);
    }

    private float CalculateRPM()
    {
        float rpm = 0;
        foreach (WheelCollider wheel in _wheels)
            rpm += wheel.rpm;

        rpm /= _wheels.Count;

        return Mathf.Abs(rpm);
    }

    public void FuelUp()
    {
        _gas = _data.maxGas;
        Debug.Log("car fueled up and now has " + _gas + " litres left");
        OnGasChange?.Invoke(_gas);
    }
}
