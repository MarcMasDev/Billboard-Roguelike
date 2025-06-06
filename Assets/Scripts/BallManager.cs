using UnityEngine;
using System.Collections.Generic;

public class BallManager : MonoBehaviour
{
    public static BallManager Instance { get; private set; }

    public List<Ball> AllBalls { get; private set; } = new List<Ball>();

    [SerializeField] private Deck defaultDeck; //Inicializar en caso de que no haya una deck asignada.

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        Instance = this;

        if (PlayerInventory.PlayerDeck == null) PlayerInventory.PlayerDeck = defaultDeck;
    }
    public void RegisterBall(Ball ball)
    {
        if (!AllBalls.Contains(ball))
        {
            AllBalls.Add(ball);
            IdentifiersManager.Instance.AddBallUI(ball);
        }
    }

    public void UnregisterBall(Ball ball)
    {
        if (AllBalls.Contains(ball))
            AllBalls.Remove(ball);
    }

    private bool AllBallsStopped()
    {
        foreach (Ball ball in AllBalls)
        {
            if (ball.rb.linearVelocity.magnitude > 0.1f)
            {
                return false;
            }
        }
        return true;
    }
    private void ResetAllScore()
    {
        foreach (Ball ball in AllBalls)
        {
            ball.ResetScore();
        }
    }
}
