using UnityEngine;
using UnityEngine.UI;

public class UiSpeedometer : MonoBehaviour
{
    [SerializeField] private Image _needle;
    [SerializeField] private float _maxSpeed = 240f;
    [SerializeField] private float _minSpeedRotation = 180f;
    [SerializeField] private float _maxSpeedRotation = -90f;
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
        float rotation = Mathf.Lerp(_minSpeedRotation, _maxSpeedRotation, Mathf.Clamp01(speed / _maxSpeed));
        _needle.rectTransform.localRotation = Quaternion.Euler(0f, 0f, rotation);
    }
}
