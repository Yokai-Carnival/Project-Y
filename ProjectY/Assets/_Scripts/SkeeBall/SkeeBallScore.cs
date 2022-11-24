using UnityEngine;

namespace SkeeBall
{
    public class SkeeBallScore : BaseScore
    {
        [Header("FeedBack")]
        [SerializeField] private ScriptableObjectEvents.StringEvent _feedBackMessage;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Ball ball))
            {
                ChangeManagerScore();
                _feedBackMessage.Raise($"{_baseScore}");
                ball.EnteredAHole();
            }
        }
    }
}
