using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int CurrentScore { get; private set; }
    private int ScoreToBeat = 0;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text scoreToBeatText;

    private void Awake()
    {
        //Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        SetScores();
    }
    public int GetScore()
    {
        return CurrentScore;
    }
    public void AddScore(int score)
    {
        CurrentScore += score;
        scoreText.text = CurrentScore.ToString("N0");
    }
    private void SetScores()
    {
        ScoreToBeat = DifficultySettings.GetScoreToBeat();
        scoreToBeatText.text = ScoreToBeat.ToString("N0");
        scoreText.text = CurrentScore.ToString("N0");
    }
}
