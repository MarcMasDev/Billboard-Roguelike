using TMPro;
using UnityEngine;

public class BallIdentifierUI : MonoBehaviour
{
    public Ball targetBall;
    [SerializeField] private Vector3 offset = new Vector3(0, 50, 0); // offset in screen space

    [SerializeField] private TMP_Text scoreText;
    private Camera mainCam;

    public void Initialize(Ball ball)
    {
        targetBall = ball;
        mainCam = Camera.main;
    }

    //Evita tenir un munt de word canvases.
    void Update()
    {
        if (!targetBall.gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            return;
        }

        Vector3 screenPos = mainCam.WorldToScreenPoint(targetBall.transform.position);
        transform.position = screenPos + offset;

        scoreText.text = targetBall.GetScore().ToString("G");
    }
}
