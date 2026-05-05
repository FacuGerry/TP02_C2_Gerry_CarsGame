using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasSystem : MonoBehaviour
{
    public event Action<float> OnGasChange;
    public event Action OnGasEmpty;

    [Header("Data")]
    [SerializeField] private CarSettingsSO _data;
    [SerializeField] private float _consumeInterval = 1f;

    [Header("Wheels")]
    [SerializeField] private List<WheelCollider> _wheels = new List<WheelCollider>();

    private float _gas;
    private float _timer;

    private IEnumerator _coroutineGas;
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

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator FuelingUp()
    {
        while (_gas < _data.maxGas)
        {
            _gas += _data.gasToCharge * Time.deltaTime;

            if (_gas >= _data.maxGas)
                _gas = _data.maxGas;

            OnGasChange?.Invoke(_gas);

            yield return null;
        }
        yield return null;
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
        if (_coroutineGas != null)
            StopCoroutine(_coroutineGas);

        _coroutineGas = FuelingUp();
        StartCoroutine(_coroutineGas);
    }

    public void StopFuelUp()
    {
        if (_coroutineGas != null)
            StopCoroutine(_coroutineGas);
    }
}
