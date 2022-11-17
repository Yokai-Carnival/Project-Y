using UnityEngine;

namespace SkeeBall
{
    public class SkeeBallManager : MonoBehaviour
    {
        [Header("Ball")]
        [SerializeField] private GameObject _ball;
        [SerializeField] private int _startingBalls = 9;
        [SerializeField] private Transform _ballSpawnPoint;
        private int _ballCount;

        [Header("Score")]
        [Tooltip("Every time the player scores this amount he gains balls back")]
        [SerializeField] private int _howMuchScoreToGainBall = 100;// Better name pls
        [SerializeField] private int _ballsToGain = 3;

        private void Start()
        {
            SpawnBalls(_startingBalls);
        }

        private void SpawnBalls(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Instantiate(_ball, _ballSpawnPoint.transform.position, Quaternion.identity);
            }
            _ballCount += amount;
        }

        //Event Listener. Listens to score manager score update
        public void GiveBalls(float scoreAmount)
        {
            if(scoreAmount % _howMuchScoreToGainBall == 0)
            {
                SpawnBalls(_ballsToGain);
            }
        }
        //Event Listener.Listens to when a ball is thrown 
        public void BallThrown()
        {
            _ballCount--;
        }
        //Event Listener. Listens to when a ball a ball stops moving 
        public void EndGame()
        {
            if(_ballCount == 0)
            {
                //EndGame
            }
        }
    }
}
