using UnityEngine;

public class Ball : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;

    protected int score = 0;
    public int GetScore() { return score; }

    public BallStats stats;

    private Vector2 lastPosition;
    private float distanceBuffer = 0f;

    [SerializeField] private GameObject fx;
    public bool diminish = false;
    [HideInInspector] public UiDisplayer ui;

    private void OnEnable()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        BallManager.Instance.RegisterBall(this);

        ResetScore();
        BallManager.OnTurnEnd += HandleTurnEnd;
    }
    private void OnDisable()
    {
        BallManager.OnTurnEnd -= HandleTurnEnd;
    }

    protected virtual void Update()
    {
        if (stats.distanceToScore > 0)
        {
            AddScoreOnDist();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (stats.bounceMultiplier > 1)
        {
            MultiplyOnBounce(collision);
        }
        else if (stats.passScore)
        {
            PassScore(collision.gameObject.GetComponent<Ball>());
        }
    }

    protected virtual void AddScoreOnDist()
    {
        float distanceMoved = Vector2.Distance(transform.position, lastPosition);
        distanceBuffer += distanceMoved;
        lastPosition = transform.position;

        while (distanceBuffer >= stats.distanceToScore)
        {
            AddScore(stats.scoreDist);
            ScoreDisplayer.Instance.ShowPopup(stats.scoreDist, ScoreType.distance, transform);
            distanceBuffer -= stats.distanceToScore;
        }
    }

    protected virtual void MultiplyOnBounce(Collision2D collision)
    {
        AddScore(Mathf.RoundToInt(score * stats.bounceMultiplier)-score);

        ScoreDisplayer.Instance.ShowPopup(stats.bounceMultiplier, ScoreType.bounce, transform);
        AudioController.Instance.Play(SoundType.hit, true, transform);
    }

    protected virtual void PassScore(Ball ball)
    {
        if (ball == null) return;

        ball.AddScore(score);
        ScoreDisplayer.Instance.ShowPopup(score, ScoreType.passScore, transform);
        ResetScore();
    }

    public void ResetScore()
    {
        score = stats.initialScore;
        lastPosition = transform.position;
        distanceBuffer = 0;

        ui.UpdatePermanentUI(score.ToString("G"));
    }


    public void AddScore(int add)
    {
        score += add;
        ui.UpdatePermanentUI(score.ToString("G"));
    }

    private void SetNegativeNumbers()
    {
        if (score > 0) score *= -1;
    }

    public virtual void OnScored()
    {
        if (stats.goldPerActiveBall > 1)
            ScoreManager.Instance.AddCoins(BallManager.Instance.PlayingBalls.Count);

        if (diminish) SetNegativeNumbers();

        Instantiate(fx, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

    private void HandleTurnEnd()
    {
        ApplyOnTurnEffects();
    }

    protected virtual void ApplyOnTurnEffects()
    {
        if (stats.turnEndMultiplier > 1)
        {
            ApplyMultiplyingOnTurnEffect();
        }
    }
    private void ApplyMultiplyingOnTurnEffect()
    {
        for (int i = 0; i < BallManager.Instance.PlayingBalls.Count; i++)
        {
            Ball toMultiply = BallManager.Instance.PlayingBalls[i];
            if (stats != toMultiply)
            {
                ScoreDisplayer.Instance.ShowPopup(stats.turnEndMultiplier, ScoreType.bounce, toMultiply.transform);
                toMultiply.AddScore(Mathf.RoundToInt(stats.turnEndMultiplier * toMultiply.GetScore()));
            }
        }
    }
}