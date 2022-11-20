using UnityEngine;
using ProjectY;
using ScriptableObjectEvents;

namespace Shooter
{
    public class TargetScore : BaseScore
    {
        [Header("Target Score")]
        private float _scoreToRaise;
        [SerializeField] private Transform _targetCenter;
        [Tooltip("From center to outer")]
        //[SerializeField] private float[] _radiusSizes = new float[3] { 2.5f, 1.5f, 1f };
        [SerializeField] private Transform[] _rings = new Transform[3];
        [SerializeField] private FloatVariable _laneMultiplier;
        private float TargetSize => _targetCenter.localScale.x;
        //Might need to map Target size multiplier from min size max size to min multiplier max multiplier  

        [Header("Timers")]
        [SerializeField] private ChangeTimerValue _addTime = new(1);
        [SerializeField] private ChangeTimerValue _removeTime = new(-1);
        [Space]
        [Header("Feed Back")]
        [SerializeField] private StringEvent _feedBackEvent;
        [Tooltip("From Center message to outside message")]
        [SerializeField] private string[] _feedBackMessage = new string[4] {"Bulls eye!", "Okay!", "Bad!", "Terrible!" };

        public System.Action Shot { get; set; }

        public override void ChangeManagerScore()
        { 
            _scored.Raise(_scoreToRaise);
            Shot?.Invoke();
        }

        public void ChangeManagerScore(Vector2 bulletPoint)
        {
            // Using vector3 might be better because we doenst use the Z axis. All we should  care for is the distance on a 2d plane
            // But this means our is locked in a rotation , where Z is depth
            float distanceFromCenter = Vector2.Distance(bulletPoint, _targetCenter.position);

            //Inner ring
            if(distanceFromCenter < RingRadius(0))
            {
                _scoreToRaise = _baseScore * 2 * _laneMultiplier /  TargetSize;
                _addTime.Change();
                _feedBackEvent.Raise(_feedBackMessage[0]);
            }
            //Inbetween ring
            else if (distanceFromCenter < RingRadius(1))
            {
                _scoreToRaise = _baseScore * _laneMultiplier / TargetSize;
                _feedBackEvent.Raise(_feedBackMessage[1]);
            }
            //Outer ring
            else if (distanceFromCenter < RingRadius(2))
            {
                _scoreToRaise = _baseScore / 2 * _laneMultiplier / TargetSize;
                _feedBackEvent.Raise(_feedBackMessage[2]);
            }
            //Too Far
            else
            {
                _scoreToRaise = -_baseScore / 2 * _laneMultiplier / TargetSize;
                _removeTime.Change();
                _feedBackEvent.Raise(_feedBackMessage[3]);
            }

            if (_baseScore < 0)
            {
                if(_scoreToRaise > 0)
                    _scoreToRaise = -_scoreToRaise;
                _feedBackEvent.Raise("Not an enemy!");
            }

            ChangeManagerScore();
        }

        private float RingRadius(int i)
        {
            return _rings[i].localScale.x * TargetSize / 2;
        }

        private void OnDrawGizmos()
        {
            if (_targetCenter == null)
                return;
            DrawTargetRadius(RingRadius(2), Color.blue);
            DrawTargetRadius(RingRadius(1), Color.red);
            DrawTargetRadius(RingRadius(0), Color.yellow);
        }

        private void DrawTargetRadius(float radius, Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawSphere(_targetCenter.position, radius);
        }
    }
}