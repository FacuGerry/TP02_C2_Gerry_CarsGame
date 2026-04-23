using TMPro;
using UnityEngine;

public class UiSpeedometer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private CarMovement _carMovement;

    private void OnEnable()
    {
        _carMovement.OnSpeedChange += ChangeSpeed;
    }

    private void OnDisable()
    {
        _carMovement.OnSpeedChange -= ChangeSpeed;
    }

    private void ChangeSpeed(float speed)
    {
        _text.text = speed.ToString("0");
    }
}
