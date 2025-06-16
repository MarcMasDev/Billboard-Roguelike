using System.Collections.Generic;
using UnityEngine;

public class IdentifiersManager : MonoBehaviour
{
    [SerializeField] private RectTransform uiParent;
    [SerializeField] private GameObject ballUI;
    [SerializeField] private GameObject holeUI;

    public static IdentifiersManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        Instance = this;
    }

    public BallIdentifierUI AddBallUI(Ball b)
    {
        GameObject uiInstance = Instantiate(ballUI, uiParent);
        BallIdentifierUI ui = uiInstance.GetComponent<BallIdentifierUI>();
        CenterRect(uiInstance.GetComponent<RectTransform>());
        ui.Initialize(b);
        return ui;
    }

    public HoleInfoUI AddHoleUI(Hole hole)
    {
        GameObject uiInstance = Instantiate(holeUI, uiParent);
        HoleInfoUI ui = uiInstance.GetComponent<HoleInfoUI>();
        CenterRect(uiInstance.GetComponent<RectTransform>());
        ui.Initialize(hole);
        return ui;
    }
    
    private void CenterRect(RectTransform rt)
    {
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
    }
}
