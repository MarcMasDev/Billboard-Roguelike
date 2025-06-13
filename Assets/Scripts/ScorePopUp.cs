using UnityEngine;
using TMPro;

public class ScorePopUp : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private RectTransform rectTransform;
    private Camera cam;

    [SerializeField] private Animator mult;
    [SerializeField] private Animator sum;
    [SerializeField] private Animator specialEffect;
    [SerializeField] private Animator score;
    [SerializeField] private Animator scoreDimished;
    [SerializeField] private Animator coin;
    [SerializeField] private Animator scoreText;
    [SerializeField] private float offsetRange = 30f;
    [HideInInspector] public bool IsInUse = false;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        cam = Camera.main;
    }

    public void Play(float amount, Transform worldTransform, ScoreType t, float speed)
    {
        IsInUse = true;

        Vector2 screenPos;
        if (worldTransform == null)
        {
            screenPos = rectTransform.position;
        }
        else
        {
            screenPos = cam.WorldToScreenPoint(worldTransform.position);
        }

        //Petit offset per evitar overlaps de score
        Vector2 randomOffset = new Vector2(
            Random.Range(-offsetRange, offsetRange),
            Random.Range(-offsetRange, offsetRange)
        );
        screenPos += randomOffset;

        rectTransform.position = screenPos;

        SetScoreText(amount ,t);

        Animator a = GetAnimator(amount, t);

        a.speed = speed;
        scoreText.speed = speed;

        a.SetTrigger("Show");
        scoreText.SetTrigger("Show");
    }

    private Animator GetAnimator(float amount, ScoreType t)
    {
        if (amount < 0) return scoreDimished;
        switch (t)
        {
            case ScoreType.bounce:
            case ScoreType.scoredMult:
                return mult;
            case ScoreType.distance:
            case ScoreType.final:
                return sum;
            case ScoreType.specialEffect:
                return specialEffect;
            case ScoreType.coin:
                return coin;
        }
        
        return score;
    }

    private void SetScoreText(float amount, ScoreType t)
    {
        string formatType = "G";
        if (t == ScoreType.coin) formatType = "C0";

        text.text = amount.ToString(formatType);

    }
}
