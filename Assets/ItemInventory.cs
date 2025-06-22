using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    public static ItemInventory Instance { get; private set; }

    private List<GameObject> items = new List<GameObject>();
    [SerializeField] private int size = 5;
    [SerializeField] private Transform parent;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void AddItem(GameObject g)
    {
        if (items.Count > size) return;
        items.Add(g);
        g.transform.SetParent(parent);
    }
}