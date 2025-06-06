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
        SpawnBall(PlayerInventory.PlayerDeck.BlackBall);
        SpawnBall(PlayerInventory.PlayerDeck.WhiteBall);
    }
    public void SpawnBall(Ball ballPrefab)
    {
        //Fer el ordre aleatori
        List<Transform> shuffled = new List<Transform>(spawnPoints);
        shuffled.Sort((a, b) => Random.Range(-1, 2));

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
}
