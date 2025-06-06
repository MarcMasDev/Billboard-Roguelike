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

    public void AddBallUI(Ball b)
    {
        GameObject uiInstance = Instantiate(uiPrefab, uiParent);
        BallIdentifierUI ui = uiInstance.GetComponent<BallIdentifierUI>();
        ui.Initialize(b);
    }
}
