using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    public static ItemInventory Instance { get; private set; }

    private List<DragableObject> items = new(4);
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

    public void AddItem(DragableObject dragable)
    {
        if (items.Count > size) return;
        items.Add(dragable);
        dragable.DragStarted += DragableOnDragStarted;
        dragable.transform.SetParent(parent);
        dragable.transform.localPosition = Vector3.zero;
    }

    private void DragableOnDragStarted(DragableObject dragable)
    {
        RemoveItem(dragable);
    }

    private void RemoveItem(DragableObject dragable)
    {
        items.Remove(dragable);
        dragable.DragStarted -= DragableOnDragStarted;
    }
}