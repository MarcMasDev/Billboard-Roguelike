using System.Collections.Generic;
using UnityEngine;

public class IdentifiersManager : MonoBehaviour
{
    [SerializeField] private RectTransform uiParent;
    [SerializeField] private GameObject uiPrefab;

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
        return ui;
    }
}
