using UnityEngine;

public class uiVisualHandler : MonoBehaviour
{
    //[HideInInspector] public bool dragging = false;
    //[HideInInspector] public bool disableInteraction = false;

    //private Vector3 targetPosition;
    //private Vector3 startingPosition;
    //private Camera mainCamera;
    //private Collider2D col;

    //[SerializeField] private float moveSpeed = 10f;

    //private Hole hole;
    //private HoleInfoUI holeUI;

    //private Ball ball;
    //private BallIdentifierUI ballUI;

    //private bool isHole = false;
    //private void Awake()
    //{
    //    hole = GetComponent<Hole>();
    //    ball = GetComponent<Ball>();

    //    isHole = hole != null;

    //    col = GetComponent<Collider2D>();
    //    mainCamera = Camera.main;
    //    startingPosition = transform.position;
    //    targetPosition = startingPosition;
    //}

    //public void Init()
    //{
    //    ShowUIInfo();
    //}

    //private void Update()
    //{
    //    if (disableInteraction) return;

    //    if (dragging)
    //    {
    //        UpdateTargetToCursor();
    //    }
    //    else if (!col.enabled)
    //    {
    //        ReturnToStartPosition();
    //    }

    //    MoveSmoothly();

    //    if (Vector2.Distance(transform.position, startingPosition) <= 0.01f && !col.enabled && !dragging) col.enabled = true;

    //    HideOnKill();
    //}

    //private void UpdateTargetToCursor()
    //{
    //    Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    //    mousePos.z = 0;
    //    targetPosition = mousePos;
    //}

    //private void ReturnToStartPosition()
    //{
    //    targetPosition = startingPosition;
    //}

    //private void MoveSmoothly()
    //{
    //    transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    //}

    //private void OnMouseDown()
    //{
    //    if (isHole)
    //    {
    //        dragging = true;
    //        HoleManager.Instance.GrabbedHole = hole;
    //        col.enabled = false;
    //        ShowUIInfo();
    //    }
    //    else
    //    {
    //        if (!disableInteraction)
    //        {
    //            SetDrag();
    //            ShowUIInfo();
    //        }
    //    }
    //}
    //private void SetDrag()
    //{
    //    dragging = true;
    //    col.enabled = false;
    //}
    //private void OnMouseUp()
    //{
    //    HoleManager.Instance.GrabbedHole = null;
        
    //    HideUIInfo();

    //    if (dragging)
    //    {
    //        Collider2D hit = Physics2D.OverlapPoint(transform.position);
    //        if (isHole && hit != null && hit.CompareTag("SellZone"))
    //        {
    //            //El item está a la zona de vendre, així que ven. Només es poden vendre els holes
    //            SellItem();
    //        }
    //        else if (!isHole && hit != null && hit.CompareTag("Ball"))
    //        {
    //            //El item está sobre un mateix item del mateix tipus (ball). Només es sobrescriuen les balls

    //            Ball hitBall = hit.GetComponent<Ball>();
    //            if (hitBall != ball && !hitBall.stats.permanent) SpawnManager.Instance.OverrideBall(hit.GetComponent<Ball>(), ball);
    //        }
    //        dragging = false;
    //    }
    //}
    //private void SellItem()
    //{
    //    ScoreManager.Instance.AddCoins(hole.stats.sellValue);
    //    Destroy(gameObject);
    //}

    //private void ShowUIInfo()
    //{
    //    if (isHole)
    //    {
    //        if (holeUI == null) holeUI = IdentifiersManager.Instance.AddHoleUI(hole);

    //        holeUI.Show();
    //        holeUI.gameObject.SetActive(true);
    //    }
    //    else
    //    {
    //        if (ballUI == null) ballUI = IdentifiersManager.Instance.AddBallUI(ball);

    //        ballUI.Show();
    //        ballUI.gameObject.SetActive(true);
    //    }
    //}
    //private void HideUIInfo()
    //{
    //    if (ball == null && hole == null) return;

    //    if (isHole && holeUI != null) holeUI.gameObject.SetActive(false);
    //    else if (ballUI != null) ballUI.ShowFullInfo(false);
    //}

    //private void HideOnKill()
    //{
    //    if (isHole && holeUI)
    //    {
    //        bool active = CheckActive(hole.gameObject);
    //        if(!active) holeUI.gameObject.SetActive(false);
    //    }
    //    else if (ballUI)
    //    {
    //        bool active = CheckActive(ball.gameObject);
    //        if (!active) ballUI.gameObject.SetActive(false);
    //    }
    //}

    //private bool CheckActive(GameObject g)
    //{
    //    if (g == null)
    //    {
    //        Destroy(gameObject);
    //        return false;
    //    }
    //    else return g.activeSelf;
    //}
}
