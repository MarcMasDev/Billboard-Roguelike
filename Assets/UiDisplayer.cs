using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiDisplayer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject target;
    private ItemData itemData;

    [Header("Generic Info Panel")] [SerializeField]
    private CanvasGroup infoPanel;

    [SerializeField] private TMP_Text itemName, itemDesc;

    [Header("Permanent UI")] [SerializeField]
    private CanvasGroup permanentUI;

    [SerializeField] private TMP_Text permanentText;
    [SerializeField] private bool usePermanent = true;
    private ScreenToWorldUI uiController;

    private bool isPointerOverUI;
    [SerializeField] private bool setOnTop = true;
    public bool draggable = false;

    public GameObject GetTarget()
    {
        return target;
    }

    public ItemData GetItemData()
    {
        return itemData;
    }

    public void Init(ItemData data, GameObject target = null)
    {
        uiController = GetComponent<ScreenToWorldUI>();
        this.target = target;

        itemData = data;
        itemName.text = itemData.itemName;

        SetInfoPanel(false);

        if (itemData.showPermanentUI && usePermanent) permanentUI.alpha = 1;
        else permanentUI.alpha = 0;

        UpdateUIInfo();
    }

    private void Update()
    {
        if (uiController != null && target) uiController.UpdatePosition(target.transform);

        bool isHoveringWorldObject = IsMouseOverTarget();

        //Comprovem si hauria d'ensenyar tant per UI com per World. Si qualsevol de les 2 es compleix i
        //El jugador no t� cap impediment (ej. esta fent drag) ensenyem la UI
        bool shouldShow = (isPointerOverUI || isHoveringWorldObject) && !PlayerInventory.hideDisplayers;
        SetInfoPanel(shouldShow);

        if (PlayerInventory.hideDisplayers) SetInfoPanel(false);
    }

    public void UpdateUIInfo()
    {
        switch (itemData.itemType)
        {
            case ItemType.hole:
                itemDesc.text = itemData.description;
                break;
            case ItemType.ball:
                Ball b = itemData.itemPrefab.GetComponent<Ball>();
                if (b != null && b.stats != null)
                {
                    itemDesc.text = itemData.itemPrefab.GetComponent<Ball>().stats.GetFormattedDescription();
                }

                break;
            default:
                itemDesc.text = itemData.description;
                break;
        }
    }

    public void UpdatePermanentUI(string newText)
    {
        permanentText.text = newText;
    }

    private void SetInfoPanel(bool isVisible)
    {
        if (isVisible)
        {
            UpdateUIInfo();
            infoPanel.alpha = 1;
            if (setOnTop) transform.SetAsLastSibling(); //Fes que sigui el displayer que es veu per sobre de tots
        }
        else infoPanel.alpha = 0;
    }

    //si es un world object utilitzem raycasts:
    private bool IsMouseOverTarget()
    {
        if (target == null)
            return false;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(mousePos);

        return hit != null && hit.gameObject == target;
    }


    //en cas contrari, utilitzem les interface de unity:
    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOverUI = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOverUI = false;
    }

    public void OnDrag()
    {
        Sell.Instance.UpdateText(itemData.sellAmount);
    }

    public bool OnDroppedOnUI(DragableObject dragable, GameObject target)
    {
        switch (target.transform.tag)
        {
            case "InventorySlot":
                ItemInventory.Instance.AddItem(dragable);
                return true;
            case "SellZone":
                Sell.Instance.SellItem(gameObject, itemData.sellAmount);
                return true;
        }

        return false;
    }

    public bool OnDroppedOnWorld(GameObject target)
    {
        switch (target.transform.tag)
        {
            case "Ball":
                if (!SpawnManager.Instance.OverrideBall(target, itemData))
                {
                    return false;
                }
                Destroy(gameObject);
                return true;
            case "SellZone":
                Sell.Instance.SellItem(gameObject, itemData.sellAmount);
                return true;
        }

        return false;
    }
}