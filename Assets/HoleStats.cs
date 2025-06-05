using UnityEngine;


[CreateAssetMenu(fileName = "HoleStats", menuName = "Scriptable Objects/HoleStats")]
public class HoleStats : ScriptableObject
{
    [Header("Scoring")]
    public int sellValue = 0;
    public int mult = 0;
    public int coins = 0;
}
