using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SkeeBall
{
    public class SkeeBallManager : MonoBehaviour
    {
        [Header("Ball")]
        [SerializeField] private Ball _ball;
        private Queue<Ball> _thrownBalls = new();
        [SerializeField] private int _startingBalls = 9;
        private int _currentBallCount;


        [SerializeField] private Transform _ballSpawnPoint;
        [SerializeField] private float _delayBetweenSpawns = 0.25f;
        private WaitForSeconds _delayWait;

        [Header("Score")]
        [Tooltip("Every time the player scores this amount he gains balls back")]
        [SerializeField] private int _howMuchScoreToGainBall = 100;// Better name pls
        [SerializeField] private int _ballsToGain = 3;

        private void Start()
        {
            _delayWait = new(_delayBetweenSpawns);
            StartCoroutine(SpawnBalls(_startingBalls));
        }

        private IEnumerator SpawnBalls(float amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Instantiate(_ball, _ballSpawnPoint.transform.position, Quaternion.identity);
                _currentBallCount++;
                yield return _delayWait;
            }
        }

        //Event Listener. Listens to score manager score update
        [ContextMenu("SpawnBalls")]
        public void GiveBallsTest() => GiveBalls(0);
        public void GiveBalls(float scoreAmount)
        {
            if(scoreAmount % _howMuchScoreToGainBall == 0)
            {
                StartCoroutine(GiveBallsCo());
            }
        }

        IEnumerator GiveBallsCo()
        {
            for (int i = 0; i < _ballsToGain; i++)
            {
                if (_thrownBalls.Count == 0)
                {
                    StartCoroutine(SpawnBalls(_ballsToGain - i));
                    yield break;
                }
                _thrownBalls.Dequeue().Appear(_ballSpawnPoint);
                _currentBallCount++;
                yield return _delayWait;
            }
        }

        //Event Listener.Listens to when a ball is thrown 
        public void BallThrown(Ball ball)
        {
            _currentBallCount--;
            _thrownBalls.Enqueue(ball);
        }

        //Event Listener. Listens to when a ball a ball stops moving 
        public void EndGame()
        {
            if(_currentBallCount == 0)
            {
                print("No more balls");
            }
        }
    }
}
