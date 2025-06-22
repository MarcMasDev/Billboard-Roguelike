using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private TMP_Text pointsNeededText;
    [SerializeField] private TMP_Text coinsNeededText;
    private int pointsNeeded = 0;
    private int coinsNeeded = 0;
    [SerializeField] private int levelSlotIndex = 0;
    [SerializeField] private int coinsIncrease = 1;
    private int levelIndex = 0;

    [SerializeField] private GameObject[] enableOnCurrent;

    public void Init()
    {
        if (anim == null) anim = GetComponent<Animator>();

        if (PlayerInventory.currentRack % PlayerInventory.PlayerDeck.difficulty.racksPerRound == 0) SetDisplayInfo();

        SetCompleted();
        SetCurrentRack();
    }

    private void SetDisplayInfo()
    {
        int baseLevelIndex = PlayerInventory.currentRack;
        levelIndex = baseLevelIndex + levelSlotIndex;

        pointsNeeded = DifficultySettings.GetLevelIndexScoreToBeat(PlayerInventory.PlayerDeck.difficulty, levelIndex);
        coinsNeeded = DifficultySettings.GetLevelIndexCurrentCoins(PlayerInventory.PlayerDeck.difficulty, levelIndex);
        UpdateVisualValues();
    }

    private void SetCompleted()
    {
        anim.SetBool("Show", levelIndex < PlayerInventory.currentRack);
    }

    private void SetCurrentRack()
    { 
        bool current = levelIndex == PlayerInventory.currentRack;

        for (int i = 0; i < enableOnCurrent.Length; i++)
        {
            enableOnCurrent[i].SetActive(current);
        }

        if (current) UpdateValues();
    }
    public void IncreaseScore(int amount)
    {
        if (amount < 0)
        {
            if (pointsNeeded <= DifficultySettings.GetScoreToBeat(PlayerInventory.PlayerDeck.difficulty))
            {
                //en caso de que sea el mínimo y quiera disminuir más, no le dejamos
                if (amount < 0) return;
            }

            coinsNeeded -= coinsIncrease;
        }
        else coinsNeeded += coinsIncrease;

        pointsNeeded += amount;
        UpdateValues();
        UpdateVisualValues();
    }

    private void UpdateValues()
    {
        DifficultySettings.CurrentScoreToBeat = pointsNeeded;
        DifficultySettings.CurrentCoinsToGain = coinsNeeded;
    }
    private void UpdateVisualValues()
    {
        pointsNeededText.text = pointsNeeded.ToString("N0");
        coinsNeededText.text = coinsNeeded.ToString("C0");
    }

    public void StartGame()
    {
        SpawnManager.Instance.SpawnDeckBalls();
    }
}
