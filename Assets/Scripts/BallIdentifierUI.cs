using TMPro;
using UnityEngine;

[RequireComponent(typeof(ScreenToWorldUI))]
public class BallIdentifierUI : MonoBehaviour
{
    //public Ball targetBall;
    //[SerializeField] private TMP_Text scoreText;

    //[Header("Full Info")]
    //[SerializeField] private TMP_Text ballTitle;
    //[SerializeField] private TMP_Text ballInfo;
    //[SerializeField] private GameObject fullInfo;

    //private ScreenToWorldUI uiController;

    //public void Initialize(Ball ball)
    //{
    //    uiController = GetComponent<ScreenToWorldUI>();
    //    targetBall = ball;
    //}


    //void Update()
    //{
    //    if (targetBall == null)
    //    {
    //        Destroy(gameObject); //Evita errors si es fa spawn sense bola o si una bola es destrueix
    //        return; //evita que es segueixi cridant el métode al mateix frame després del destroy
    //    }

    //    scoreText.text = targetBall.GetScore().ToString("G");
    //    uiController.UpdateInfo(targetBall.transform);
    //}

    //public void ShowFullInfo(bool show)
    //{
    //    fullInfo.SetActive(show);
    //}
    //public void Show()
    //{
    //    ballTitle.text = targetBall.stats.ballName;
    //    ballInfo.text = targetBall.stats.explanation;
    //}
}
