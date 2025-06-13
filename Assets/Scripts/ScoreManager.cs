using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int CurrentScore { get; private set; }
    private int ScoreToBeat = 0;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text scoreToBeatText;

    [Header("Events")]
    public UnityEvent onWin;
    public UnityEvent onLose;

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
        CurrentScore = 0;
        ScoreToBeat = DifficultySettings.GetScoreToBeat(PlayerInventory.PlayerDeck.difficulty);
        scoreToBeatText.text = ScoreToBeat.ToString("N0");
        scoreText.text = CurrentScore.ToString("N0");
    }

    //Condicions de victoria i derrota
    private void Update()
    {
        if (CurrentScore >= ScoreToBeat)
        {
            GoToNextRound();
        }
        else if (PlayerInventory.shots <= 0)
        {
            Loose();
        }
    }

    private void GoToNextRound()
    {
        onWin?.Invoke();
        PlayerInventory.NextRound();
        SetScores();
    }
    private void Loose()
    {
        onLose?.Invoke();
    }
}
