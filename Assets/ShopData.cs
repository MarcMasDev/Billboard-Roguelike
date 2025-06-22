using UnityEngine;

public enum ItemType
{
    hole,
    ball
}

[System.Serializable]
public struct ItemData
{
    public ItemType itemType;
    public bool showPermanentUI;

    public string itemName;
    [TextArea] public string description;

    public Sprite icon;
    public GameObject itemPrefab;

    public int buyPrice;
    public int sellAmount;
}


[CreateAssetMenu(fileName = "ShopData", menuName = "Scriptable Objects/ShopData")]
public class ShopData : ScriptableObject
{

    public HoleStats[] purchasableHoles;
    public BallStats[] purchasableBalls;
}
