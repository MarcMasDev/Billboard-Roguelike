using System.Collections.Generic;
public static class PlayerInventory
{
    public static Deck PlayerDeck;
    public static int Coins = 0;
    public static int currentRack = 0;
    public static int shots = 6;
    public static int initShots = 6;
    public static bool hideDisplayers = false;

    public static void NextRound()
    {
        currentRack++;
        shots = initShots;
    }
}
