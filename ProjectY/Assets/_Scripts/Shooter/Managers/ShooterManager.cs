using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectY;
using UnityEngine.UI;

namespace Shooter
{
    public class ShooterManager : MonoBehaviour
    {
        [Header("Targets")]
        [SerializeField] private List<TargetFlipper> _targetFlippersPool = new();
        private List<TargetFlipper> _currentTargetFlipped = new();
        [SerializeField] private Vector2Int _batchRange = new(1,6);


        [Header("Timers")]
        [SerializeField] private Timer _endGameTimer;
        [SerializeField] private FloatVariable _currentTime;
        [SerializeField] private Timer _flipTargets;
        [SerializeField] private Timer _flipTargetsBack;

        private void OnEnable()
        {
            _endGameTimer.TimeEvent += EndGame;
            _flipTargets.TimeEvent += FlipTargets;
            _flipTargetsBack.TimeEvent += FlipTargetsBack;
        }

        private void OnDisable()
        {
            _endGameTimer.TimeEvent -= EndGame;
            _flipTargets.TimeEvent -= FlipTargets;
            _flipTargetsBack.TimeEvent -= FlipTargetsBack;
        }

        private void Update()
        {
            float startTime = _endGameTimer.Time;
            _currentTime.Value = MathHelper.Map(_endGameTimer.ElapsedTime, 0, startTime, startTime, 0);
        }

        public void EndGame()
        {
            DisableTimers(_flipTargets);
            DisableTimers(_flipTargetsBack);
            DisableTimers(_endGameTimer);
            _currentTime.Value = 0;
        }

        private void DisableTimers(Timer timer)
        {
            timer.StopAndReset();
            timer.enabled = false;
        }

        [ContextMenu("Flip")]
        public void FlipTargets()
        {
            _flipTargets.StopAndReset();
            _flipTargetsBack.Continue();

            int poolSize = _targetFlippersPool.Count;
            int amountToFlip = Random.Range(_batchRange.x, _batchRange.y);

            for (int i = 0; i < amountToFlip; i++)
            {
                int index = Random.Range(0, poolSize);
                TargetFlipper iFlipper = _targetFlippersPool[index];
                RemoveFromPool(iFlipper);
                iFlipper.Flip();
                poolSize--;
            }
        }

        [ContextMenu("FlipBack")]
        public void FlipTargetsBack()
        {
            _flipTargets.Continue();
            _flipTargetsBack.StopAndReset();
            for (int i = _currentTargetFlipped.Count - 1; i >= 0; i--)
            {
                TargetFlipper iFlipper = _currentTargetFlipped[i];
                AddBackToPool(iFlipper);
                iFlipper.FlipBack();
            }
        }

        private void RemoveFromPool(TargetFlipper iFlipper)
        {
            _currentTargetFlipped.Add(iFlipper);
            _targetFlippersPool.Remove(iFlipper);
        }

        private void AddBackToPool(TargetFlipper iFlipper)
        {
            _targetFlippersPool.Add(iFlipper);
            _currentTargetFlipped.Remove(iFlipper);
        }

        //Event Listener
        public void AddBackToPoolPublic(TargetFlipper flipper)
        {
            AddBackToPool(flipper);
            if(_currentTargetFlipped.Count == 0)
            {
                _flipTargets.Continue();
                _flipTargetsBack.StopAndReset();

                // Can Add A special score here 
                // Like if the player shot all targets before the timer to flip back
            }
        }
    }
}
