using UnityEngine;

public class MenuGroup : MonoBehaviour
{
    private Animator anim;
    [HideInInspector] public CanvasGroup cg;
    private void Awake()
    {
        cg = GetComponent<CanvasGroup>();
        anim = GetComponent<Animator>();
    }

    public void SetVisible(bool show)
    {
        if(anim) anim.SetBool("Show", show);
    }
}
