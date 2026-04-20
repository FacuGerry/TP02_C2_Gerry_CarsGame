using TMPro;
using UnityEngine;

public class UiSpeedometer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        CarMovement.OnSpeedChange += ChangeSpeed;
    }

    private void OnDisable()
    {
        CarMovement.OnSpeedChange -= ChangeSpeed;
    }

    private void ChangeSpeed(float speed)
    {
        _text.text = speed.ToString("0");
    }
}
