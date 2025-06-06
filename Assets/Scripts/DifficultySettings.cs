using UnityEngine;

public static class DifficultySettings
{
    private static int racksPerRound = 3;
    private static float startValue = 200;
    private static float roundsMultiplier = 10;
    private static float rackMultiplier = 2.5f;

    public static int GetScoreToBeat()
    {
        int roundIndex = PlayerInventory.currentRack / racksPerRound;
        int withinRound = PlayerInventory.currentRack % racksPerRound;

        float blockValue = startValue * Mathf.Pow(roundsMultiplier, roundIndex);
        float value = blockValue * Mathf.Pow(rackMultiplier, withinRound);

        return Mathf.RoundToInt(value);
    }
}