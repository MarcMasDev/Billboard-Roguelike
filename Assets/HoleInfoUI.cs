using UnityEngine;
using TMPro;

[RequireComponent(typeof(ScreenToWorldUI))]
public class HoleInfoUI : MonoBehaviour
{
    [SerializeField] private Hole targetHole;

    [SerializeField] private TMP_Text multiplierText;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text explanationText;

    private ScreenToWorldUI uiController;

    public void Initialize(Hole hole)
    {
        targetHole = hole;
    }

    void Update()
    {
        if (targetHole == null) Destroy(gameObject); //Evita errors si es fa spawn sense bola o si una bola es destrueix

        uiController.UpdatePosition(targetHole.transform);
    }
    public void Show()
    {
        if (uiController == null) uiController = GetComponent<ScreenToWorldUI>();

        uiController.UpdatePosition(targetHole.transform);
        explanationText.text = targetHole.explanation;
        coinsText.text = targetHole.stats.coins.ToString("C0");
        multiplierText.text = targetHole.stats.mult.ToString("G");
    }
}
