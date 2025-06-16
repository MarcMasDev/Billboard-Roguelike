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

    private uiVisualHandler uiHandler;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        uiHandler = GetComponent<uiVisualHandler>();

        enabled = false;
    }

    //Activar cuando se hace spawn.
    public void PutBallToTable()
    {
        if (!BallManager.Instance.PlayingBalls.Contains(this)) BallManager.Instance.RegisterBall(this);

        uiHandler.disableInteraction = true;
        ResetBall();
    }
    private void ResetBall()
    {
        uiHandler.Init();

        var col = GetComponent<Collider2D>();
        if (col != null) col.enabled = true;

        score = stats.initialScore;
        ResetScore();
    }
    protected virtual void Update()
    {
        if (stats.distanceToScore > 0)
        {
            AddScoreOnDist();
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
            ApplySpecialEffect(ScoreType.distance);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (stats.bounceMultiplier > 0 && collision.gameObject.GetComponent<WhiteBall>() == null)
        {
            int newScore = Mathf.RoundToInt(score * stats.bounceMultiplier);
            score = newScore;
            ScoreDisplayer.Instance.ShowPopup(stats.bounceMultiplier, ScoreType.bounce, transform);

            ApplySpecialEffect(ScoreType.bounce, collision.gameObject);
            AudioController.Instance.Play(SoundType.hit, true, transform);
        }

    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hole"))
        {
            if (diminish) SetNegativeNumbers();
            InHole();
        }
    }
    private void InHole()
    {
        Instantiate(fx, transform.position, Quaternion.identity);
        Kill();
    }
    protected virtual void Kill()
    {
        gameObject.SetActive(false);
    }
    public void ResetScore()
    {
        score = stats.initialScore;
        lastPosition = transform.position;
        distanceBuffer = 0;
    }

    public void AddScore(int add)
    {
        score += add;
    }

    protected virtual void ApplySpecialEffect(ScoreType scoreType, GameObject colliding = null)
    {

    }

    private void SetNegativeNumbers()
    {
        if (score > 0) score *= -1;
    }

    public virtual void OnScored()
    {

    }
}