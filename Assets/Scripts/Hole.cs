using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public enum ScoreType
{
    bounce,
    distance,
    passScore,
    coin,
    scored,
    scoredMult,
    shot,
    final
}

[RequireComponent(typeof(uiVisualHandler))]
public class Hole : MonoBehaviour
{
    public HoleStats stats;
    public string explanation = "";

    private uiVisualHandler holeDisplayer;

    private void Awake()
    {
        holeDisplayer = GetComponent<uiVisualHandler>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (holeDisplayer.dragging) return;

        Ball ball = collision.GetComponent<Ball>();

        if (ball != null)
        {
            for (int i = 0; i < ball.stats.scoreAmount; i++)
            {
                SetScore(ball);
                SetCoins(ball);
                ball.OnScored();

                ShowMultScoreVisuals();
                AudioController.Instance.Play(SoundType.score, true, transform);
            }
        }

    }
    protected virtual void SetScore(Ball ball)
    {
        CountScore(ball.GetScore() * stats.mult);
    }
    protected virtual void SetCoins(Ball ball)
    {
        ScoreManager.Instance.AddCoins(ball.diminish ? -stats.coins : stats.coins);
    }
    protected virtual void CountScore(int finalScore)
    {
        ScoreManager.Instance.AddScore(finalScore);
    }
    private void ShowMultScoreVisuals()
    {
        ScoreDisplayer.Instance.ShowPopup(stats.mult, ScoreType.scoredMult, transform);
    }
}
