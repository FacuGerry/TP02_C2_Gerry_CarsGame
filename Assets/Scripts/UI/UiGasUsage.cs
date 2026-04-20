using TMPro;
using UnityEngine;

public class UiGasUsage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        GasSystem.OnGasChange += ChangeGas;
    }

    private void OnDisable()
    {
        GasSystem.OnGasChange -= ChangeGas;
    }

    private void ChangeGas(float gas)
    {
        _text.text = gas.ToString("0.00");
    }
}
