using UnityEngine;

[CreateAssetMenu(fileName = "Difficulty", menuName = "Scriptable Objects/Difficulty")]
public class Difficulty : ScriptableObject
{
    public int racksPerRound = 3;
    public float startValue = 200;
    public float roundsMultiplier = 10;
    public float rackMultiplier = 2.5f;
}
