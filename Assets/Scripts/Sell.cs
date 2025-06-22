using TMPro;
using UnityEngine;

public class Sell : MonoBehaviour
{
    [SerializeField] private TMP_Text sellTXT;
    public CanvasGroup cg;

    public static Sell Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        Instance = this;

        if (!cg) cg = GetComponent<CanvasGroup>();
    }

    public void SellItem(GameObject item, int sellAmount)
    {
        ScoreManager.Instance.AddCoins(sellAmount);
        Destroy(item);
    }

    public void UpdateText(int amount)
    {
        sellTXT.text = amount.ToString("C0");
    }
}
