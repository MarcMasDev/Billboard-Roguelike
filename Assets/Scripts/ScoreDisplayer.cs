using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public enum ScorePlace
{
    onPlace,
    coinPlace,
    handPlace,
    finalScore
}

[System.Serializable]
public struct ScoringType
{
    public ScoreType scoreType;
    public ScorePlace place;
    public float appearanceSpeed;
}

[System.Serializable]
public class PoolParents
{
    public ScorePlace scorePlace;
    public RectTransform popupParent;
    [HideInInspector] public Queue<ScorePopUp> pool = new Queue<ScorePopUp>();
}

public class ScoreDisplayer : MonoBehaviour
{
    public static ScoreDisplayer Instance;

    [SerializeField] private ScorePopUp popupPrefab;
    [SerializeField] private int poolSize = 20;
    [SerializeField] private float destroyTime = 0.5f;
    [SerializeField] private ScoringType[] scorePlacings;
    [SerializeField] private PoolParents[] poolParents;

    private Dictionary<ScorePlace, PoolParents> poolLookup;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        poolLookup = new Dictionary<ScorePlace, PoolParents>();
        foreach (var poolParent in poolParents)
        {
            poolLookup[poolParent.scorePlace] = poolParent;
            FillQueue(poolParent);
        }
    }

    private void FillQueue(PoolParents poolParent)
    {
        for (int i = 0; i < poolSize; i++)
        {
            ScorePopUp popup = Instantiate(popupPrefab, poolParent.popupParent);
            popup.gameObject.SetActive(false);
            poolParent.pool.Enqueue(popup);
        }
    }

    public void ShowPopup(float scoreAmount, ScoreType scoreType, Transform worldPosition = null)
    {
        ScoringType scoringType = GetPlacing(scoreType);
        if (!poolLookup.TryGetValue(scoringType.place, out PoolParents poolParent))
        {
            Debug.LogError($"No pool parent found for ScorePlace: {scoringType.place}");
            return;
        }

        ScorePopUp popup = GetPopupFromPool(poolParent);
        popup.gameObject.SetActive(true);
        popup.Play(scoreAmount, worldPosition, scoreType, scoringType.appearanceSpeed);
        StartCoroutine(ReturnToPoolAfter(popup, destroyTime, poolParent));
    }

    private ScorePopUp GetPopupFromPool(PoolParents poolParent)
    {
        if (poolParent.pool.Count > 0)
        {
            return poolParent.pool.Dequeue();
        }

        //Crea una instancia nova si el pool està buit
        ScorePopUp newPopup = Instantiate(popupPrefab, poolParent.popupParent);
        return newPopup;
    }

    private IEnumerator ReturnToPoolAfter(ScorePopUp popup, float delay, PoolParents poolParent)
    {
        yield return new WaitForSeconds(delay);

        if (popup != null)
        {
            popup.gameObject.SetActive(false);
            poolParent.pool.Enqueue(popup);
        }
    }

    private ScoringType GetPlacing(ScoreType t)
    {
        foreach (var placing in scorePlacings)
        {
            if (placing.scoreType == t) return placing;
        }

        Debug.LogWarning($"No scoring type found for {t}, using default");
        return scorePlacings[0];
    }
}