using System;
using UnityEngine;
using UnityEngine.UI;

public class UiSelectionScreenButtons : MonoBehaviour
{
    public event Action<bool> OnVehiclePressed;         // If true: right pressed. If false: left pressed.
    public event Action<bool> OnTrackPressed;           // If true: right pressed. If false: left pressed.
    public event Action OnPlayPressed;

    [Header("Vehicles")]
    [SerializeField] private Button _btnVehicleLeft;
    [SerializeField] private Button _btnVehicleRight;

    [Header("Tracks")]
    [SerializeField] private Button _btnTrackLeft;
    [SerializeField] private Button _btnTrackRight;

    [Header("Button play")]
    [SerializeField] private Button _btnPlay;

    private void Start()
    {
        _btnVehicleLeft.onClick.AddListener(VehicleLeftClicked);
        _btnVehicleRight.onClick.AddListener(VehicleRightClicked);

        _btnTrackLeft.onClick.AddListener(TrackLeftClicked);
        _btnTrackRight.onClick.AddListener(TrackRightClicked);

        _btnPlay.onClick.AddListener(PlayClicked);
    }   

    private void OnDestroy()
    {
        _btnVehicleLeft.onClick.RemoveAllListeners();
        _btnVehicleRight.onClick.RemoveAllListeners();

        _btnTrackLeft.onClick.RemoveAllListeners();
        _btnTrackRight.onClick.RemoveAllListeners();

        _btnPlay.onClick.RemoveAllListeners();
    }   

    private void VehicleLeftClicked()
    {
        OnVehiclePressed?.Invoke(false);
    }

    private void VehicleRightClicked()
    {
        OnVehiclePressed?.Invoke(true);
    }

    private void TrackLeftClicked()
    {
        OnTrackPressed?.Invoke(false);
    }

    private void TrackRightClicked()
    {
        OnTrackPressed?.Invoke(true);
    }

    private void PlayClicked()
    {
        OnPlayPressed?.Invoke();
    }
}