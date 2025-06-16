using UnityEngine;


[CreateAssetMenu(fileName = "BallStats", menuName = "Scriptable Objects/BallStats")]
public class BallStats : ScriptableObject
{
    public GameObject ballPrefab;
    public string ballName = "Ball";

    [Header("Scoring")]
    public int initialScore = 100;


    [Header("Special Effect")]
    public string explanation = "Gives its score when scored.";
    public bool permanent = false;
    public int scoreDist = 0;
    public float bounceMultiplier = 1;
    public float distanceToScore = 0.02f;
    public int scoreAmount = 1;
    public int goldPerActiveBall = 2;
    public int multiplyActiveBallsOnShot = 2;
    public bool passScore = false;
}
