using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomButtonVisuals : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Sprite pressedSprite;
    private Sprite initSprite;
    private Image buttonImage;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        initSprite = buttonImage.sprite;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (pressedSprite != null)
            buttonImage.sprite = pressedSprite;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (initSprite != null)
            buttonImage.sprite = initSprite;
    }
}
