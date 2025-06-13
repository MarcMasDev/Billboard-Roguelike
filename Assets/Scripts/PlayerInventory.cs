using System.Collections.Generic;
public static class PlayerInventory
{
    public static Deck PlayerDeck;
    public static int Coins = 0;
    public static int currentRack = 1;
    public static int shots = 5;
    public static int initShots = 5;

    public static void NextRound()
    {
        currentRack++;
        shots = initShots;
    }
}
