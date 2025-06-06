using TMPro;
using UnityEngine;

public class SellHoleUI : MonoBehaviour
{
    [SerializeField] private TMP_Text sellTXT;
    private CanvasGroup cg;

    private void Start()
    {
        cg = GetComponent<CanvasGroup>();
    }
    private void Update()
    {
        Hole grabbedHole = HoleManager.Instance.GrabbedHole;
        if (grabbedHole)
        {
            cg.alpha = 1;
            sellTXT.text = grabbedHole.stats.sellValue.ToString("C0");
        }
        else { cg.alpha = 0; }
    }
}
