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

    public static int GetLevelIndexCurrentCoins(Difficulty settings, int level)
    {
        int levelIndex = level % settings.racksPerRound;

        return settings.coinsPerRack[levelIndex];
    }

    public static int GetLevelIndexScoreToBeat(Difficulty settings, int level)
    {
        int roundIndex = level / settings.racksPerRound;
        int withinRound = level % settings.racksPerRound;

        float blockValue = settings.startValue * Mathf.Pow(settings.roundsMultiplier, roundIndex);
        float value = blockValue * Mathf.Pow(settings.rackMultiplier, withinRound);

        return Mathf.RoundToInt(value);
    }

    public static int CurrentScoreToBeat = 1;
    public static int CurrentCoinsToGain = 1;
}