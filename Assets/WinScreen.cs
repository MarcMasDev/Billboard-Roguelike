using UnityEngine;

public class WinScreen : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private LevelUI[] levels;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        ShowWinMenu(true);
    }
    public void ShowWinMenu(bool show)
    {
        anim.SetBool("Show", show);
    }

    public void ShowLevels()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].Init();
        }
    }
}
