using UnityEngine;

[CreateAssetMenu(fileName = "ObjectData", menuName = "Objects/ObjectData")]

public class ObjectDataSO : ScriptableObject
{
    public int damage;
    public int spawnCount;
    public GameObject prefab;

    [Header("If object has to move:")]
    public float travelDistance;
    public float travelHeight;
    public float travelDuration;
}
