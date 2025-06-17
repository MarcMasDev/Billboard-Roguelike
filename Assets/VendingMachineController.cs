using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class VendingMachineController : MonoBehaviour
{
    [SerializeField] private TMP_Text screenText;
    [SerializeField] private ShopData shopData;


    [SerializeField] private VendingSlot[] slots;
    [SerializeField] private GameObject holeUIGameobject;
    [SerializeField] private GameObject ballUIGameobject;

    [Range(0, 1f)][SerializeField] private float holeChance = 0.5f; //Nos permite controlar el drop rate


    private string numberSelected ="";
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();

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
        for (int i = 0; i < slots[index].amountOfItems; i++)
        {
            int randomIndex = GetRandomIndex(ItemType.ball);
            slots[index].AddItem(i, shopData.purchasableItems[i], ballUIGameobject);

            if (holeChance < Random.value)
            {
                //int randomIndex = GetRandomIndex(ItemType.hole);
            }
            else
            {
                //int randomIndex = GetRandomIndex(ItemType.ball);
                //slots[index].AddItem(i, ballUIGameobject);
            }
        }
    }
    private int GetCurrentNumber()
    {
        return int.Parse(numberSelected); //Converteix el string a int per no tenir un array de strings
    }

    private int GetRandomIndex(ItemType itemT)
    {
        int count = 0;

        for (int i = 0; i < shopData.purchasableItems.Length; i++)
        {
            if (shopData.purchasableItems[i].itemType == itemT) count++;
        }
        return Random.Range(0, count);
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
