using UnityEngine;

[CreateAssetMenu(fileName = "NpcData", menuName = "Npc/NpcData")]

public class StatsDataSO : ScriptableObject
{
    [Header("Stats")]
    public int life;

    [Header("Attacking")]
    public float distanceToShoot;
    public float heightToShoot;
    public float shootingSpeed;
    public float bulletSpeed;
    public int shootingDamage;
}