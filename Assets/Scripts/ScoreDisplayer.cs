using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < poolParents.Length; i++)
        {
            FillQueue(poolParents[i].pool, poolParents[i].popupParent);
        }
    }

    private void FillQueue(Queue<ScorePopUp> queue, RectTransform parent)
    {
        for (int i = 0; i < poolSize; i++)
        {
            ScorePopUp popup = Instantiate(popupPrefab, parent);
            popup.gameObject.SetActive(false);
            queue.Enqueue(popup);
        }
    }
    public void ShowPopup(float scoreAmount, Vector2 worldPosition, ScoreType t)
    {
        ScorePopUp popup;

        ScoringType scorePlace = GetPlacing(t);
        PoolParents poolParent = GetPoolParent(scorePlace.place);

        if (poolParent.pool.Count > 0) popup = poolParent.pool.Dequeue();
        else popup = Instantiate(popupPrefab, poolParent.popupParent);

        popup.gameObject.SetActive(true);
        popup.Play(scoreAmount, worldPosition, t, scorePlace.appearanceSpeed);
        StartCoroutine(ReturnToPoolAfter(popup, destroyTime, scorePlace.place, poolParent));
    }

    private System.Collections.IEnumerator ReturnToPoolAfter(ScorePopUp popup, float delay, ScorePlace returnToPlace, PoolParents p)
    {
        yield return new WaitForSeconds(delay);
        popup.gameObject.SetActive(false);
        p.pool.Enqueue(popup);
    }

    private ScoringType GetPlacing(ScoreType t)
    {
        for (int i = 0; i < scorePlacings.Length; i++)
        {
            if (t == scorePlacings[i].scoreType) return scorePlacings[i];
        }
        return scorePlacings[0];
    }

    private PoolParents GetPoolParent(ScorePlace t)
    {
        for (int i = 0; i < poolParents.Length; i++)
        {
            if (poolParents[i].scorePlace == t) return poolParents[i];
        }
        return poolParents[0];
    }
}
