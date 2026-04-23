using TMPro;
using UnityEngine;

public class UiGasUsage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GasSystem _gasSystem;

    private void OnEnable()
    {
        _gasSystem.OnGasChange += ChangeGas;
    }

    private void OnDisable()
    {
        _gasSystem.OnGasChange -= ChangeGas;
    }

    private void ChangeGas(float gas)
    {
        _text.text = gas.ToString("0.00");
    }
}
