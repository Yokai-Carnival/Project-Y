using UnityEngine;
using ScriptableObjectEvents;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private float _score;
    [SerializeField] private FloatEvent _scoreUpdated;

    //Event Listener to when score event is Raised
    public void ChangeScore(float amount)
    {
        _score += amount;
        _scoreUpdated.Raise(_score);
    }

    //Todo Turn Score into tickets
}
