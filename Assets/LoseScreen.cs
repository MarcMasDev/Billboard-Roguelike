using TMPro;
using UnityEngine;

public class LoseScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _score;

    public void SetScore()
    {
        _score.text = $"Score: {DifficultySettings.CurrentScoreToBeat}";
        PlayerInventory.Reset();
    }
}
