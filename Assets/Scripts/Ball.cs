using UnityEngine;

[RequireComponent (typeof(uiVisualHandler))]
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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (!BallManager.Instance.PlayingBalls.Contains(this)) BallManager.Instance.RegisterBall(this);
    }

    private void OnEnable()
    {
        ResetScore();
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
            score += stats.scoreDist;
            ScoreDisplayer.Instance.ShowPopup(stats.scoreDist, ScoreType.distance, transform);
            distanceBuffer -= stats.distanceToScore;
        }
    }

    protected virtual void MultiplyOnBounce(Collision2D collision)
    {
        int newScore = Mathf.RoundToInt(score * stats.bounceMultiplier);
        score = newScore;
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
    }
    public void OnShoot()
    {
        if (stats.multiplyActiveBallsOnShot > 1)
        {
            for (int i = 0; i < BallManager.Instance.PlayingBalls.Count; i++)
            {
                Ball toMultiply = BallManager.Instance.PlayingBalls[i];

                if (stats != toMultiply) toMultiply.AddScore(stats.multiplyActiveBallsOnShot);
            }
        }
    }

    public void AddScore(int add)
    {
        score += add;
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
}