using System.Collections.Generic;
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

    private void Start()
    {
        foreach (Ball ball in PlayerInventory.PlayerDeck.balls)
        {
            SpawnBall(ball);
        }
    }
    public void SpawnBall(Ball ballPrefab)
    {
        //Fer el ordre aleatori
        List<Transform> shuffled = Shuffle(new List<Transform>(spawnPoints));

        foreach (Transform point in shuffled)
        {
            Collider2D hit = Physics2D.OverlapCircle(point.position, checkRadius, ballLayer);
            if (hit == null)
            {
                Instantiate(ballPrefab, point.position, Quaternion.identity);
                return;
            }
        }

        Debug.LogWarning("No free spawn point found");
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
