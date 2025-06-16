using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class VendingMachineController : MonoBehaviour
{
    [SerializeField] private TMP_Text screenText;
    [SerializeField] private ShopData shopData;


    [SerializeField] private VendingSlot[] slots;
    private string numberSelected ="";
    private Animator anim;

    private int maxItems;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        maxItems = shopData.purchasableItems.Length;

        for (int i = 0; i < slots.Length; i++)
        {
            RollItems(i);
        }
    }

    public void AddNumber(int index)
    {
        if(numberSelected.Length >= 2) 
        {
            anim.SetTrigger("Error");
            UpdateText("");
        }
        else
            UpdateText(numberSelected + index.ToString());
    }

    public void Buy()
    {
        int num = GetCurrentNumber();
        
        if (num >= 0) slots[num].BuySlot();
        else
        UpdateText("");
    }

    public void Roll()
    {
        RollItems(GetCurrentNumber());
        UpdateText("");
    }

    private void RollItems(int index)
    {
        slots[index].ResetItems(); //esborra els items guardats

        FillSlot(index);
    }
    private void FillSlot(int index)
    {
        int randomIndex = GetRandomIndex(maxItems);
        ShopItem selectedItem = shopData.purchasableItems[randomIndex];
        slots[index].AddItems(selectedItem);
    }
    private int GetCurrentNumber()
    {
        return int.Parse(numberSelected); //Converteix el string a int per no tenir un array de strings
    }

    private int GetRandomIndex(int max)
    {
        return Random.Range(0, max);
    }
    private void UpdateText(string t)
    {
        numberSelected = t;
        screenText.text = numberSelected;
    }

    private void TriggerError()
    {
        anim.SetTrigger("Error");
        numberSelected = ""; //El setting del texto se hará des de la animación.
    }
}
