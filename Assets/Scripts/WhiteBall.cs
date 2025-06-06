using Unity.VisualScripting;
using UnityEngine;

public class WhiteBall : Ball
{
    private Vector2 dragStart;
    private Vector2 dragEnd;
    private bool isDragging = false;

    [SerializeField] private float power = 10f;
    [SerializeField] private float maxPower = 100f;
    [SerializeField] private LineRenderer aimLine;

    protected override void Update()
    {
        base.Update();
        if (rb.linearVelocity.magnitude < 0.1f) //Només es pot apuntar si la bola está quieta.
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (aimLine == null) aimLine = GameObject.Find("ShootDisplayer").GetComponent<LineRenderer>();
            OnDrag();
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            OnDragging();
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            OnStopDrag();
        }
    }
    private void OnDrag()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Només si s'arrastra a prop de la bola
        if (Vector2.Distance(mouseWorldPos, rb.position) < 0.5f)
        {
            isDragging = true;
            dragStart = mouseWorldPos;
        }
    }
    private void OnDragging()
    {
        dragEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (aimLine) LineVisuals(rb.position, dragEnd);
    }
    private void OnStopDrag()
    {
        isDragging = false;
        Vector2 forceDir = dragStart - dragEnd;

        float forceMagnitude = Mathf.Min(maxPower, forceDir.magnitude);
        rb.AddForce(forceDir.normalized * forceMagnitude * power, ForceMode2D.Impulse);

        PlayerInventory.shots -= 1;
        ScoreDisplayer.Instance.ShowPopup(-1, transform.position, ScoreType.hand);
        LineVisuals(rb.position, rb.position);
    }
    private void LineVisuals(Vector2 startPos, Vector2 endPos)
    {
        aimLine.SetPosition(0, startPos);
        aimLine.SetPosition(1, endPos);
    }

    protected override void ApplySpecialEffect(ScoreType scoreType, GameObject colliding = null)
    {
        if (scoreType == ScoreType.bounce)
        {
            Ball ball = colliding.GetComponent<Ball>();

            if (ball != null)
            {
                ball.AddScore(score);
                ScoreDisplayer.Instance.ShowPopup(score, transform.position, ScoreType.specialEffect);
                ResetScore();
            }
            else
            {
                int newScore = Mathf.RoundToInt(score * stats.bounceMultiplier);
                score = newScore;
                ScoreDisplayer.Instance.ShowPopup(stats.bounceMultiplier, transform.position, ScoreType.bounce);
            }
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        ApplySpecialEffect(ScoreType.bounce, collision.gameObject);
    }


    protected override void Kill()
    {
        SpawnManager.Instance.SpawnBall(this);
        base.Kill();
    }
}
