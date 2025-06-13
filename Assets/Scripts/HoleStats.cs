using UnityEngine;


[CreateAssetMenu(fileName = "HoleStats", menuName = "Scriptable Objects/HoleStats")]
public class HoleStats : ScriptableObject
{
    [Header("Shop")]
    public int buyValue = 0;
    public int sellValue = 0;

    [Header("Scoring")]
    public int mult = 0;
    public int coins = 0;
}
