using UnityEngine;

public enum ScoreType
{
    bounce,
    distance,
    specialEffect,
    coin,
    scored,
    scoredMult,
    hand,
    final
}
public class Hole : MonoBehaviour
{
    private bool dragging;
    private Vector3 targetPosition;
    private Vector3 startingPosition;
    private Camera mainCamera;
    private Collider2D col;

    [SerializeField] private float moveSpeed = 10f;
    public HoleStats stats;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        mainCamera = Camera.main;
        startingPosition = transform.position;
        targetPosition = startingPosition;
    }

    private void Update()
    {
        if (dragging)
        {
            UpdateTargetToCursor();
        }
        else
        {
            ReturnToStartPosition();
        }

        MoveSmoothly();

        if (Vector2.Distance(transform.position, startingPosition) <= 0.01f && !col.enabled &&!dragging) col.enabled = true;
    }

    private void UpdateTargetToCursor()
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        targetPosition = mousePos;
    }

    private void ReturnToStartPosition()
    {
        targetPosition = startingPosition;
    }

    private void MoveSmoothly()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        dragging = true;
        HoleManager.Instance.GrabbedHole = this;
        col.enabled = false;
    }

    private void OnMouseUp()
    {
        dragging = false;
        HoleManager.Instance.GrabbedHole = null;

        Collider2D hit = Physics2D.OverlapPoint(transform.position);
        if (hit != null && hit.CompareTag("SellZone"))
        {
            //El forat está a la zona de vendre, així que ven
            PlayerInventory.Coins += stats.sellValue;
            gameObject.SetActive(false);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Ball ball = collision.GetComponent<Ball>();
        bool diminish = ball.diminish;
        int ballScore = ball.GetScore();
        int finalScore = ballScore * stats.mult;

        ScoreManager.Instance.AddScore(finalScore);

        ScoreDisplayer.Instance.ShowPopup(ballScore, transform.position, ScoreType.scored);
        ScoreDisplayer.Instance.ShowPopup(stats.mult, transform.position, ScoreType.scoredMult);
        ScoreDisplayer.Instance.ShowPopup(finalScore, transform.position, ScoreType.final);

        if (diminish)
        {
            PlayerInventory.Coins -= stats.coins;
            ScoreDisplayer.Instance.ShowPopup(-stats.coins, transform.position, ScoreType.coin);
        }
        else
        {
            PlayerInventory.Coins += stats.coins;
            ScoreDisplayer.Instance.ShowPopup(stats.coins, transform.position, ScoreType.coin);
        }
    }
}
