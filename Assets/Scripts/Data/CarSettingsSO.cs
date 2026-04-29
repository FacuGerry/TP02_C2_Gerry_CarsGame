using UnityEngine;

[CreateAssetMenu(fileName = "CarSettings", menuName = "Car/CarSettings")]

public class CarSettingsSO : ScriptableObject
{
    public float maxSpeed;
    public float weight;
    public float horsePower;
    public float brakePower;
    public float steeringPower;
    public float timeToResetRotation;

    [Header("Durability settings")]
    public float maxDurability;
    public float durabilityToCharge;

    [Header("Gas settings")]
    public float maxGas;
    public float gasUsage;
    public float gasToCharge;
}