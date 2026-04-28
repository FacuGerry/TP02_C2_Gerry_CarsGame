using UnityEngine;

[CreateAssetMenu(fileName = "Score", menuName = "GameSettings/Score")]

public class ScoreSO : ScriptableObject
{
    public int score;
    public int maxScore;
}
