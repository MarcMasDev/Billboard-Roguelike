using TMPro;
using UnityEngine;

public class InventoryUIUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text shots;
    private void Update()
    {
        shots.text = PlayerInventory.shots.ToString();
    }
}
