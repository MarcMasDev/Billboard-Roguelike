using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private float checkRadius = 0.5f;
    [SerializeField] private LayerMask ballLayer;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SpawnDeckBalls()
    {
        PlayerInventory.hideDisplayers = false;

        if (BallManager.Instance.PlayingBalls.Count <= 0) { InitBalls(); return; }

        for (int i = 0; i < BallManager.Instance.PlayingBalls.Count; i++)
        {
            if (i < BallManager.Instance.PlayingBalls.Count)
            {
                ActivateBall(BallManager.Instance.PlayingBalls[i]);
            }
            else
            {
                CreateBall(BallManager.Instance.PlayingBalls[i].gameObject);
            }
        }
    }
    private void InitBalls()
    {
        for (int i = 0; i < BallManager.Instance.defaultDeck.balls.Length; i++)
        { 
            CreateBall(BallManager.Instance.defaultDeck.balls[i].ballPrefab);
        }
    }

    public void ActivateBall(Ball ball)
    {
        ball.gameObject.SetActive(true);
        ball.transform.position = GetSpawnPoint();
        ball.ui.gameObject.SetActive(true);
    }

    public bool OverrideBall(GameObject target, ItemData toSpawn)
    {
        var ball = target.GetComponent<Ball>();
        if (ball.stats.permanent)
            return false;
        
        BallManager.Instance.UnregisterBall(ball);
        IdentifiersManager.Instance.RemoveItemUI(target);
        Destroy(target);
        CreateBall(toSpawn.itemPrefab);

        return true;
    }

    public GameObject CreateBall(GameObject ballPrefab)
    {
        return Instantiate(ballPrefab, GetSpawnPoint(), Quaternion.identity);
    }

    public Vector2 GetSpawnPoint()
    {
        //Fer el ordre aleatori
        List<Transform> shuffled = Shuffle(new List<Transform>(spawnPoints));

        foreach (Transform point in shuffled)
        {
            Collider2D hit = Physics2D.OverlapCircle(point.position, checkRadius, ballLayer);
            if (hit == null)
            {
                return point.position;
            }
        }

        Debug.LogWarning("No free spawn point found");
        return Vector2.zero;
    }

    //Randomiza una llista
    List<T> Shuffle<T>(List<T> original)
    {
        List<T> copy = new List<T>(original);
        List<T> result = new List<T>();

        while (copy.Count > 0)
        {
            int index = Random.Range(0, copy.Count);
            result.Add(copy[index]);
            copy.RemoveAt(index);
        }

        return result;
    }
}
