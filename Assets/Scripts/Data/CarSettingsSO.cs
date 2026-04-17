using UnityEngine;

[CreateAssetMenu(fileName = "CarSettings", menuName = "Car/CarSettings")]

public class CarSettingsSO : ScriptableObject
{
    public float maxDurability;
    public float maxSpeed;
    public float weight;
    public float horsePower;
    public float brakePower;
    public float steeringPower;
    public KeyCode restartCar;
}