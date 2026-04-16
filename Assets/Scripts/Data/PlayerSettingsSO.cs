using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Player/PlayerSettings")]

public class PlayerSettingsSO : ScriptableObject
{
    public float horsePower;
    public float brakePower;
    public float steeringPower;
}