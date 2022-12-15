using UnityEngine;

using ScriptableObjectEvents;
namespace ProjectY
{
    public class DynamicTimer : Timer
    {
        public override float ElapsedTime { get => base.ElapsedTime; set => base.ElapsedTime = value; }
        public override float Time { get => _timer; protected set => _timer.SetValue_ = value; }
        public float Multiplier { get => _multiplier; set => _multiplier = value; }

        [SerializeField] private FloatVariable _timer;
        [SerializeField] private float _multiplier;

        protected override void TickTime()
        {
            ElapsedTime += UnityEngine.Time.deltaTime * _multiplier;
        }
    }

 }