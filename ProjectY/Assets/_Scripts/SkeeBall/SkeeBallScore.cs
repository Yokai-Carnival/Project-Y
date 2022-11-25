using UnityEngine;

namespace SkeeBall
{
    public class SkeeBallScore : BaseScore
    {
        [Header("FeedBack")]
        [SerializeField] private ScriptableObjectEvents.StringEvent _feedBackMessage;

        protected virtual string FeedBackMessage { get => $"{_baseScore}"; }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Ball ball))
            {
                if (ball.Scored)
                    return;
                ChangeManagerScore();
                _feedBackMessage.Raise(FeedBackMessage);
                ball.EnteredAHole();
            }
        }
    }
}
