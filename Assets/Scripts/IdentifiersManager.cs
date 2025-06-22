using System.Collections.Generic;
using UnityEngine;

public class IdentifiersManager : MonoBehaviour
{
    [SerializeField] private RectTransform uiParent;
    [SerializeField] private GameObject uiPrefab;

    private List<UiDisplayer> uiDisplayers = new List<UiDisplayer>();

    public static IdentifiersManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        Instance = this;
    }

    public UiDisplayer AddItemUI(ItemData data, GameObject toFollow)
    {
        GameObject uiInstance = Instantiate(uiPrefab, uiParent);
        UiDisplayer ui = uiInstance.GetComponent<UiDisplayer>();
        ui.Init(data, toFollow);
        uiDisplayers.Add(ui);
        return ui;
    }

    public void RemoveItemUI(GameObject targetRemoved)
    {
        UiDisplayer displayerToRemove = uiDisplayers.Find(d => d.GetTarget() == targetRemoved);

        if (displayerToRemove != null)
        {
            uiDisplayers.Remove(displayerToRemove);
            Destroy(displayerToRemove.gameObject);
        }
    }
}
