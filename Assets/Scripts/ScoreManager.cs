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

    [SerializeField] private TMP_Text coinsText;

    [Header("Events")]
    public UnityEvent onWin;
    public UnityEvent onLose;

    private bool won = false;

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

        ScoreDisplayer.Instance.ShowPopup(score, ScoreType.final);
        scoreText.text = CurrentScore.ToString("N0");

        won = false;
    }
    public void AddCoins(int amount)
    {
        PlayerInventory.Coins += amount;

        ScoreDisplayer.Instance.ShowPopup(amount, ScoreType.coin);
        coinsText.text = PlayerInventory.Coins.ToString("C0");
    }
    public void SetScores()
    {
        CurrentScore = 0;
        ScoreToBeat = DifficultySettings.CurrentScoreToBeat;
        scoreToBeatText.text = ScoreToBeat.ToString("N0");
        scoreText.text = CurrentScore.ToString("N0");
    }

    //Condicions de victoria i derrota
    private void Update()
    {
        if (BallManager.Instance.AllBallsStopped())
        {
            if (CurrentScore >= ScoreToBeat && !won)
            {
                won = true;
                GoToNextRound();
            }
            else if (PlayerInventory.shots <= 0)
            {
                Loose();
            }
        }

        if (Input.GetKeyDown(KeyCode.A)) { CurrentScore = ScoreToBeat; }
    }

    private void GoToNextRound()
    {
        onWin?.Invoke();
        AddCoins(DifficultySettings.CurrentCoinsToGain);
        PlayerInventory.NextRound();
    }
    private void Loose()
    {
        onLose?.Invoke();
    }
}
