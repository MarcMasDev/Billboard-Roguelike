using UnityEngine;
public class Ball : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;

    protected int score = 0;
    public int GetScore() { return score; }

    [SerializeField] protected BallStats stats;

    private Vector2 lastPosition;
    private float distanceBuffer = 0f;

    [SerializeField] private GameObject fx;
    public bool diminish = false;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        score = stats.initialScore;
        lastPosition = rb.position;
        BallManager.Instance.RegisterBall(this);
        ResetScore();
    }

    protected virtual void Update()
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
        //No multiplicar si rep la colisi� de la bola blanca
        if (collision.gameObject.GetComponent<WhiteBall>() != null) return;

        int newScore = Mathf.RoundToInt(score * stats.bounceMultiplier);
        score = newScore;
        ScoreDisplayer.Instance.ShowPopup(stats.bounceMultiplier, ScoreType.bounce, transform);

        ApplySpecialEffect(ScoreType.bounce, collision.gameObject);
        AudioController.Instance.Play(SoundType.hit, true, transform);
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
        BallManager.Instance.UnregisterBall(this);
        gameObject.SetActive(false);
    }
    public void ResetScore()
    {
        score = stats.initialScore;
        lastPosition = rb.position;
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
}