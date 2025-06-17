using UnityEngine;
using System;
using System.Collections.Generic;

public class BallManager : MonoBehaviour
{
    public static BallManager Instance { get; private set; }
    public List<Ball> PlayingBalls { get; private set; } = new List<Ball>(); //solo las instanciadas

    public Deck defaultDeck; //Inicializar en caso de que no haya una deck asignada.


    public static event Action OnTurnEnd;
    private bool hasTriggeredTurnEnd = false;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        Instance = this;

        if (PlayerInventory.PlayerDeck == null) PlayerInventory.PlayerDeck = defaultDeck;
    }
    public void RegisterBall(Ball ball)
    {
        if (!PlayingBalls.Contains(ball))
        {
            PlayingBalls.Add(ball);
            ball.ui = IdentifiersManager.Instance.AddItemUI(ball.stats.itemInfo, ball.gameObject);
        }
    }
    public void UnregisterBall(Ball ball)
    {
        if (PlayingBalls.Contains(ball)) PlayingBalls.Remove(ball);
        Destroy(ball);
    }

    public void KillRemainingBalls()
    {
        for (int i = 0;i < PlayingBalls.Count; i++)
        {
            if (PlayingBalls[i].gameObject.activeSelf) PlayingBalls[i].gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        HandleTurnCalls();
    }
    private void HandleTurnCalls()
    {
        if (AllBallsStopped())
        {
            if (!hasTriggeredTurnEnd)
            {
                hasTriggeredTurnEnd = true;
                OnTurnEnd?.Invoke();
            }
        }
        else hasTriggeredTurnEnd = false;
    }

    public bool AllBallsStopped()
    {
        foreach (Ball ball in PlayingBalls)
        {
            if (ball.rb.linearVelocity.magnitude > 0.1f)
            {
                return false;
            }
        }
        return true;
    }
}
