using System.Collections.Generic;
using UnityEngine;

public class VendingSlot : MonoBehaviour
{
    private ShopItem[] itemSlots = new ShopItem[3];
    private GameObject[] spawnedItems = new GameObject[3];

    [Range(0, 1f)][SerializeField] private float holeChance = 0.5f; //Nos permite hacer que cada shop slot tenga un custom drop rate

    public void AddItems(ShopItem item)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i] = item;

            if (holeChance < Random.value)
            {
                spawnedItems[i] = Instantiate(item.hole.gameObject, transform);
            }
            else
            {
                spawnedItems[i] = Instantiate(item.ball.gameObject, transform);
            }
        }

    }

    public void BuySlot()
    {
        Debug.Log("You purchased!");
    }

    public void ResetItems()
    {
        for (int i = 0; i < spawnedItems.Length; i++) 
        {
            if (spawnedItems[i]) Destroy(spawnedItems[i]);
        }
    }

}
