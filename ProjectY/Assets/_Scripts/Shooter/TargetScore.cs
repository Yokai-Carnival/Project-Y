using UnityEngine;

namespace Shooter
{
    public class TargetScore : BaseScore
    {
        public System.Action Shot { get; set; }
        public override void ChangeManagerScore()
        {
            float s = _baseScore;
            _scored.Raise(s);

            Shot?.Invoke();
        }
    }
}