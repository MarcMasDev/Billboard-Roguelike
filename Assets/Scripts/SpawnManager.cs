using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

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
        for (int i = 0; i < BallManager.Instance.BallTemplates.Count; i++)
        {
            if (i < BallManager.Instance.PlayingBalls.Count)
            {
                ActivateBall(BallManager.Instance.PlayingBalls[i]);
            }
            else
            {
                CreateBall(BallManager.Instance.BallTemplates[i]);
            }
        }
    }

    public void ActivateBall(Ball ball)
    {
        ball.gameObject.SetActive(true);
        ball.transform.position = GetSpawnPoint();

        ball.PutBallToTable();
    }

    public void OverrideBall(Ball bToOverride, Ball bToSpawn)
    {
        BallManager.Instance.UnregisterBall(bToOverride);

        //Ens guardem la world scale del item en world UI
        Vector3 worldScale = bToSpawn.transform.localScale;

        bToSpawn.enabled = true;
        bToSpawn.transform.SetParent(null);
        bToSpawn.transform.position = bToOverride.transform.position;
        bToSpawn.transform.localScale = worldScale;

        bToSpawn.PutBallToTable();
        Destroy(bToOverride.gameObject);
    }

    private void CreateBall(Ball ballPrefab)
    {
        Ball b = Instantiate(ballPrefab, GetSpawnPoint(), Quaternion.identity).GetComponent<Ball>();
        b.enabled = true;
        b.PutBallToTable();
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
