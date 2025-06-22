using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private MenuGroup[] menus;
    [SerializeField] private int indexEnabled = 0;
    private void Start()
    {
        EnableDefaultIndex();
    }
    public void EnableDefaultIndex()
    {
        EnableIndex(indexEnabled);
    }
    public void EnableIndex(int index)
    {
        DisableAll();

        EnableCG(menus[index].cg, true);
        menus[index].SetVisible(true);    
    }
    private void DisableAll()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            EnableCG(menus[i].cg, false);
            menus[i].SetVisible(false);
        }
    }

    private void EnableCG(CanvasGroup cg, bool show)
    {
        if (cg == null) return;

        cg.interactable = show;
        cg.blocksRaycasts = show;
    }
}
