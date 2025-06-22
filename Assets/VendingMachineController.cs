using System.Collections.Generic;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class VendingMachineController : MonoBehaviour
{
    [SerializeField] private TMP_Text screenText;
    [SerializeField] private ShopData shopData;


    [SerializeField] private VendingSlot[] slots;
    [SerializeField] private Transform obtainedSlotParent;
    [SerializeField] private GameObject holeUIGameobject;
    [SerializeField] private GameObject ballUIGameobject;
    [SerializeField] private Vector3 itemSize;

    [Range(0, 1f)][SerializeField] private float holeChance = 0.5f; //Nos permite controlar el drop rate
    private bool isBuying = false;

    private string numberSelected ="";
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        Init();
    }

    public void Init()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            RollItems(i);
        }
    }

    public void AddNumber(int index)
    {
        if (isBuying) return;

        if (numberSelected == "--")
        {
            UpdateText("");
        }
        
        if(numberSelected.Length >= 2) 
        {
            TriggerError();
        }
        else
            UpdateText(numberSelected + index.ToString());
    }
    public void ShowPuchasedItemsBuy()
    {
        int num = GetCurrentNumber();

        for (int i = 0; i < slots[num].amountOfItems; i++)
        {
            GameObject toAdd = slots[num].GetSlotItem(i);
            toAdd.transform.SetParent(obtainedSlotParent);
            toAdd.transform.localScale = itemSize;
            UiDisplayer ui = toAdd.GetComponent<UiDisplayer>();
            if (ui) ui.draggable = true;
        }

        slots[num].ClearItems(false);
    }
    public void EndItemsBuy()
    {            
        //Resetea els items i el slot
        RollItems(GetCurrentNumber());

        isBuying = false;
        UpdateText("--");
    }
    public void Buy()
    {
        int num = GetCurrentNumber();

        if (num >= 0 && num < slots.Length && slots[num].GetCost() <= PlayerInventory.Coins)
        {
            isBuying = true;

            //Comen�a la animaci�
            slots[num].BuySlot();
        }
    }

    public void Roll()
    {
        RollItems(GetCurrentNumber());
        UpdateText("--");
    }

    private void RollItems(int index)
    {
        slots[index].ResetItems(); //esborra els items guardats

        FillSlot(index);
    }
    private void FillSlot(int index)
    {
        for (int i = 0; i < slots[index].amountOfItems; i++)
        {
            if (holeChance > Random.value)
            {
                int randomIndex = Random.Range(0, shopData.purchasableHoles.Length);
                slots[index].AddItem(i, shopData.purchasableHoles[randomIndex].itemInfo, holeUIGameobject);
            }
            else
            {
                int randomIndex = Random.Range(0, shopData.purchasableBalls.Length);
                slots[index].AddItem(i, shopData.purchasableBalls[randomIndex].itemInfo, ballUIGameobject);
            }
        }
    }
    private int GetCurrentNumber()
    {
        if (numberSelected == "--") return -1;
        return int.Parse(numberSelected); //Converteix el string a int per no tenir un array de strings
    }

    private void UpdateText(string t)
    {
        numberSelected = t;
        screenText.text = numberSelected;
    }

    private void TriggerError()
    {
        anim.SetTrigger("Error");
        UpdateText("--");
        AudioController.Instance.Play(SoundType.error);
    }
}
