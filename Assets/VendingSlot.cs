using UnityEngine;
using UnityEngine.UI;

public class VendingSlot : MonoBehaviour
{
    private GameObject[] slotItems;
    [SerializeField] private Transform shopItemsParent;

    public int amountOfItems = 3;

    private void Awake()
    {
        slotItems = new GameObject[amountOfItems];
    }

    public void AddItem(int index, ItemData item, GameObject uiToSpawn)
    {
        slotItems[index] = Instantiate(uiToSpawn, shopItemsParent);
        
        //Init item
        slotItems[index].GetComponent<Image>().sprite = item.icon;
        slotItems[index].GetComponent<UiDisplayer>().Init(item, uiToSpawn);
    }

    public void BuySlot()
    {
        Debug.Log("You purchased!");
    }

    public void ResetItems()
    {
        for (int i = 0; i < slotItems.Length; i++) 
        {
            if (slotItems[i]) Destroy(slotItems[i]);
        }
    }

}
