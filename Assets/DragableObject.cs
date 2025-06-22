using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform), typeof(CanvasGroup))]
public class DragableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private UiDisplayer displayer;

    private Vector2 originalPosition;
    private Transform originalParent;
    private bool isDragging;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        displayer = GetComponent<UiDisplayer>();

        ShowGrab(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!displayer.draggable) return;

        originalPosition = rectTransform.anchoredPosition;
        originalParent = transform.parent;
        canvasGroup.blocksRaycasts = false;
        isDragging = true;

        //Mou a dalt de tot mentres fa dragging
        transform.SetParent(canvas.transform);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out Vector2 localPoint);

        rectTransform.localPosition = localPoint;

        ShowGrab(true);
        displayer.OnDrag();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        canvasGroup.blocksRaycasts = true;
        isDragging = false;
        ShowGrab(false);

        //Mira si es deix en el world
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D worldHit = Physics2D.OverlapPoint(mouseWorldPos);
        if (worldHit != null)
        {
            displayer.OnDroppedOnWorld(worldHit.gameObject);
            return;
        }

        //Mira si s'ha deixat en la ui
        GameObject uiTarget = eventData.pointerCurrentRaycast.gameObject;
        if (uiTarget != null && uiTarget != gameObject)
        {
            displayer.OnDroppedOnUI(uiTarget);
            return;
        }



        //Si no, torna a la posició inicial
        rectTransform.anchoredPosition = originalPosition;
        transform.SetParent(originalParent);
    }

    private void ShowGrab(bool show)
    {
        int alpha = show ? 1 : 0;
        int spriteAlpha = show ? 255 : 0;

        Sell.Instance.cg.alpha = alpha;
        GameObject[] toActiveObject = GameObject.FindGameObjectsWithTag("Dragging");
        foreach (var obj in toActiveObject)
        {
            CanvasGroup cg = obj.GetComponent<CanvasGroup>();
            if (cg)
            {
                cg.alpha = alpha;
            }
            else
            {
                SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
                if (sr)
                {
                    sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, spriteAlpha);
                }
            }
        }
    }
}