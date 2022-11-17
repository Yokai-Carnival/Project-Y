using UnityEngine;
using ScriptableObjectEvents;

public class BaseScore : MonoBehaviour
{
    [Header("Base Score")]
    [SerializeField] protected FloatEvent _scored;
    [SerializeField] protected float _baseScore;

    public virtual void ChangeManagerScore()
    {
        _scored.Raise(_baseScore);
    }
}