using UnityEngine;

[CreateAssetMenu(fileName = "CarSelected", menuName = "Car/CarSelected")]

public class SelectionsSO : ScriptableObject
{
    public CarSettingsSO car;
    public TrackSettingsSO track;
}
