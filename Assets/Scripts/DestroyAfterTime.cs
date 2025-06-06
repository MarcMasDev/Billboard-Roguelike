using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float m_Time;
    void Start()
    {
        Destroy(gameObject, m_Time);
    }
}
