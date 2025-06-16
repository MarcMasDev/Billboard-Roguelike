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
    public string ballName = "Ball";

    [Header("Scoring")]
    public int initialScore = 100;
    public int scoreDist = 1;
    public float bounceMultiplier = 1.25f;
    public float distanceToScore = 0.02f;
    public int scoreAmount = 2;

    [Header("Special Effect")]
    public SpecialEffectType specialEffect;
    public string explanation = "Gives its score when scored.";
    public bool permanent = false;

}
