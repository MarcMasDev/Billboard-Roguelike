using UnityEngine;


[CreateAssetMenu(fileName = "HoleStats", menuName = "Scriptable Objects/HoleStats")]
public class HoleStats : ScriptableObject
{
    public GameObject holePrefab;
    public GameObject holeUIPrefab;

    [Header("Shop/UI Info")]
    public ItemData itemInfo;

    [Header("Shop")]
    public int sellValue = 0;

    [Header("Scoring")]
    public int mult = 0;
    public int coins = 0;
}
