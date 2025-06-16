using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WhiteBall : Ball
{
    private Vector2 dragStart;
    private Vector2 dragEnd;
    private bool isDragging = false;

    [Header("Force Settings")]
    [SerializeField] private float power = 10f;
    [SerializeField] private float maxDragDistance = 3f;
    [SerializeField] private AnimationCurve powerCurve = AnimationCurve.Linear(0, 0, 1, 1);

    [Header("UI References")]
    [SerializeField] private LineRenderer aimLine;
    [SerializeField] private Slider powerDisplayer;

    [Header("Gradient Settings")]
    [SerializeField] private Color lowColor = Color.red;
    [SerializeField] private Color midColor = Color.yellow;
    [SerializeField] private Color highColor = Color.green;
    [SerializeField] private float midPoint = 0.5f; // Value between 0-1 where midColor is strongest
    private Material gradientMaterial;

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
        SetupUIReferences();
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
        UpdateDragEnd();
        UpdateAimVisual();
        UpdatePowerSlider();
    }
    private void OnStopDrag()
    {
        isDragging = false;
        ApplyForce();
        ResetVisuals();
    }
    private void UpdateDragEnd()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dragVector = mouseWorldPos - dragStart;

        if (dragVector.magnitude > maxDragDistance)
        {
            dragVector = dragVector.normalized * maxDragDistance;
        }

        dragEnd = dragStart + dragVector;
    }
    private void UpdatePowerSlider()
    {
        if (powerDisplayer == null) return;

        float dragDistance = Mathf.Clamp((dragStart - dragEnd).magnitude, 0f, maxDragDistance);
        float normalized = dragDistance / maxDragDistance;


        powerDisplayer.value = normalized;

        //Mostrar la distancia restant, ja que el power està modificat per la curve i es una mica missleading
        if (gradientMaterial != null)
        {
            Color currentColor;
            if (normalized < midPoint)
            {
                //Aplica gradient de low a mid
                float t = normalized / midPoint;
                currentColor = Color.Lerp(lowColor, midColor, t);
            }
            else
            {
                //Aplica gradient de mid a high
                float t = (normalized - midPoint) / (1f - midPoint);
                currentColor = Color.Lerp(midColor, highColor, t);
            }

            gradientMaterial.SetColor("_Color", currentColor);
        }
    }

    private void ApplyForce()
    {
        Vector2 forceDir = dragStart - dragEnd;

        float dragDistance = Mathf.Clamp(forceDir.magnitude, 0f, maxDragDistance);
        float normalizedDistance = dragDistance / maxDragDistance;
        float powerMultiplier = powerCurve.Evaluate(normalizedDistance);

        rb.AddForce(forceDir.normalized * powerMultiplier * power, ForceMode2D.Impulse);

        DecreaseShot();
    }
    private void DecreaseShot()
    {
        PlayerInventory.shots -= 1;

        BallManager.Instance.ApplyOnShotEffects();
        ScoreDisplayer.Instance.ShowPopup(-1, ScoreType.shot);
        AudioController.Instance.Play(SoundType.hitBig, true, transform);
    }
    private void UpdateAimVisual()
    {
        if (aimLine)
        {
            LineVisuals(rb.position, dragEnd);
        }
    }

    private void LineVisuals(Vector2 startPos, Vector2 endPos)
    {
        aimLine.SetPosition(0, startPos);
        aimLine.SetPosition(1, endPos);
    }

    private void ResetVisuals()
    {
        LineVisuals(rb.position, rb.position); //amaga l'apuntat
        if (powerDisplayer) powerDisplayer.value = 0f;
    }

    public override void OnScored()
    {
        base.OnScored();
        SpawnManager.Instance.ActivateBall(this);
    }

    private void SetupUIReferences()
    {
        if (aimLine == null)
            aimLine = GameObject.Find("ShootDisplayer")?.GetComponent<LineRenderer>();

        if (powerDisplayer == null)
            powerDisplayer = GameObject.Find("PowerDisplayer")?.GetComponent<Slider>();

        if (gradientMaterial == null && powerDisplayer?.fillRect != null)
        {
            Image fillImage = powerDisplayer.fillRect.GetComponent<Image>();
            if (fillImage != null && fillImage.material != null)
            {
                gradientMaterial = Instantiate(fillImage.material);
                fillImage.material = gradientMaterial;
            }
        }
    }
}
