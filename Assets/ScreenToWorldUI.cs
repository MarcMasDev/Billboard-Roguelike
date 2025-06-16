using UnityEngine;

//Evita tenir un munt de world canvases.
public class ScreenToWorldUI : MonoBehaviour
{
    private Camera mainCam;
    [SerializeField] private Vector3 offset = new Vector3(0, 50, 0); //offset en screen space

    public void UpdateInfo(Transform t)
    {
        if (t == null || !t.gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            return;
        }
        if (!mainCam) mainCam = Camera.main;

        Vector3 screenPos = mainCam.WorldToScreenPoint(t.transform.position);
        transform.position = screenPos + offset;
    }
}
