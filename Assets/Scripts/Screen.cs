using UnityEngine;
using UnityEngine.EventSystems;

public class Screen : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject[] toDisable;
    [SerializeField] private GameObject[] toEnable;

    public void OnPointerClick(PointerEventData eventData)
    {
        DisableObjects();
        EnableObjects();
    }

    private void DisableObjects(bool enabled = false)
    {
        for (int i = 0; i < toDisable.Length; i++)
        {
            toDisable[i].SetActive(enabled);
        }
    }

    private void EnableObjects(bool enabled = true)
    {
        for (int i = 0; i < toEnable.Length; i++)
        {
            toEnable[i].SetActive(enabled);
        }
    }
}
