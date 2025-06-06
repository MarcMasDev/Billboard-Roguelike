using UnityEngine;
public enum SpecialEffectType
{
    None,
    WhiteBall,
    BlackBall
}


[CreateAssetMenu(fileName = "BallStats", menuName = "Scriptable Objects/BallStats")]
public class BallStats : ScriptableObject
{
    [Header("Scoring")]
    public int initialScore = 100;
    public int scoreDist = 1;
    public float bounceMultiplier = 1.25f;
    public float distanceToScore = 0.02f;

    [Header("Special Effect")]
    public SpecialEffectType specialEffect;
}
