using System.Collections.Generic;
using UnityEngine;

public class HoleManager : MonoBehaviour
{
    public static HoleManager Instance { get; private set; }

    public List<Ball> AllBalls { get; private set; } = new List<Ball>();

    public Hole GrabbedHole;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        Instance = this;
    }
}
