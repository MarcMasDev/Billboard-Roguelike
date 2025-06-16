using UnityEngine;
using System.Collections.Generic;

public class BallManager : MonoBehaviour
{
    public static BallManager Instance { get; private set; }
    public List<Ball> PlayingBalls { get; private set; } = new List<Ball>(); //solo las instanciadas

    [SerializeField] private Deck defaultDeck; //Inicializar en caso de que no haya una deck asignada.

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        Instance = this;

        for (int i = 0; i < defaultDeck.balls.Length; i++)
        {
            PlayingBalls.Add(defaultDeck.balls[i]);
        }

        if (PlayerInventory.PlayerDeck == null) PlayerInventory.PlayerDeck = defaultDeck;
    }
    public void RegisterBall(Ball ball)
    {
        if (!PlayingBalls.Contains(ball))
        {
            PlayingBalls.Add(ball);
        }
    }
    public void UnregisterBall(Ball ball)
    {
        if (PlayingBalls.Contains(ball)) PlayingBalls.Remove(ball);
        Destroy(ball);
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
    
    public void ApplyOnShotEffects()
    {
        foreach (Ball ball in PlayingBalls)
        {
            ball.OnShoot();
        }
    }

    public void KillRemainingBalls()
    {
        for (int i = 0;i < PlayingBalls.Count; i++)
        {
            if (PlayingBalls[i].gameObject.activeSelf) PlayingBalls[i].gameObject.SetActive(false);
        }
    }
}
