using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "GameSettings/GameData")]

public class GameDataSO : ScriptableObject
{
    public int minEnemies;
    public int maxEnemies;
}
