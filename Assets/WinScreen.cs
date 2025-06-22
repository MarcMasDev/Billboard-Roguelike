using TMPro;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private LevelUI[] levels;
    [SerializeField] private TMP_Text roundTitleTxt;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        ShowWinMenu(true);
    }
    public void ShowWinMenu(bool show)
    {
        anim.SetBool("Show", show);
        roundTitleTxt.SetText($"Round - {(PlayerInventory.Round+1):00}");
    }

    public void ShowLevels()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].Init();
        }
    }
}
