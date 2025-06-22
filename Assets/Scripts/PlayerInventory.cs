public static class PlayerInventory
{
    public static Deck PlayerDeck;
    public static int Coins = 0;
    public static int currentRack = 0;
    public static int shots = 6;
    public static int initShots = 6;
    public static bool hideDisplayers = false;

    public static int Round => currentRack / 3;

    public static void NextRound()
    {
        currentRack++;
        shots = initShots;
    }
    
    public static void Reset()
    {
        Coins = 0;
        currentRack = 0;
        shots = 6;
        initShots = 6;
    }
}
