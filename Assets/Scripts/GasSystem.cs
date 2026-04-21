using System;
using System.Collections.Generic;
using UnityEngine;

public class GasSystem : MonoBehaviour
{
    public static event Action<float> OnGasChange;
    public event Action OnGasEmpty;

    [Header("Data")]
    [SerializeField] private CarSettingsSO _data;
    [SerializeField] private float _consumeInterval = 1f;

    [Header("Wheels")]
    [SerializeField] private List<WheelCollider> _wheels = new List<WheelCollider>();

    private float _gas;
    private float _timer;

    private void Start()
    {
        _gas = _data.maxGas;
        OnGasChange?.Invoke(_gas);
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer < _consumeInterval)
            return;

        _timer = 0f;

        _gas -= _data.gasUsage * CalculateRPM() / 1000f * Time.deltaTime;
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
