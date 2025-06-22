using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VendingSlot : MonoBehaviour
{
    private GameObject[] slotItems = new GameObject[3];
    [SerializeField] private Transform shopItemsParent;
    [SerializeField] private Animator anim;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private VendingMachineController controller;

    public int amountOfItems = 3;
    private int slotCost = 0;
    public int GetCost()
    {
        return slotCost;
    }
    public GameObject GetSlotItem(int index)
    {
        return slotItems[index];
    }

    private void Awake()
    {
        Init();
    }
    public void StartBuy()
    {
        controller.ShowPuchasedItemsBuy();
    }
    public void EndBuy()
    {
        controller.EndItemsBuy();
    }
    public void AddItem(int index, ItemData item, GameObject uiToSpawn)
    {
        slotItems[index] = Instantiate(uiToSpawn, shopItemsParent);

        //Init item
        slotItems[index].GetComponent<Image>().sprite = item.icon;
        slotItems[index].GetComponentInChildren<UiDisplayer>().Init(item, uiToSpawn);

        //Set Cost
        AddPrice(item);
    }

    public void BuySlot()
    {
        ScoreManager.Instance.AddCoins(-slotCost);
        anim.SetTrigger("Purchased");

        //for (int i = 0; i < slotData.Length; i++)
        //{
        //    if (PlayerInventory.InventoryDeck.Count + amountOfItems < PlayerInventory.InventorySize)
        //    {
        //        PlayerInventory.InventoryDeck.Add(slotData[i]);
        //    }
        //}
    }

    public void ClearItems(bool destroy = false)
    {
        for (int i = 0; i < slotItems.Length; i++)
        {
            if (slotItems[i])
            {
                if(destroy)
                    Destroy(slotItems[i]);
                slotItems[i] = null;
            }
        }
    }

    public void ResetItems()
    {
        ClearItems(true);
        ResetPrice();
    }

    private void Init()
    {
        if (!shopItemsParent) shopItemsParent = transform;
        if (!anim) anim = GetComponent<Animator>();
        for (int i = 0; i < slotItems.Length; i++)
        {
            Destroy(slotItems[i]);
        }
        ResetItems();
    }

    private void AddPrice(ItemData item)
    {
        slotCost += item.buyPrice;
        costText.text = slotCost.ToString("C0");
    }
    private void ResetPrice()
    {
        slotCost = 0;
    }
}
