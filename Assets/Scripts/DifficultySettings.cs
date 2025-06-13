using UnityEngine;

public static class DifficultySettings
{
    public static int GetScoreToBeat(Difficulty settings)
    {
        int roundIndex = PlayerInventory.currentRack / settings.racksPerRound;
        int withinRound = PlayerInventory.currentRack % settings.racksPerRound;

        float blockValue = settings.startValue * Mathf.Pow(settings.roundsMultiplier, roundIndex);
        float value = blockValue * Mathf.Pow(settings.rackMultiplier, withinRound);

        return Mathf.RoundToInt(value);
    }
}