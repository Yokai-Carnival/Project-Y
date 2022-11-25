using UnityEngine;
using ScriptableObjectEvents;

namespace SkeeBall
{
    public class SkeeBallSpecialScore : SkeeBallScore
    {
        [Header("Special Score")]
        [SerializeField] private SkeeBallScore[] _scores;
        [SerializeField] private VoidEvent _specialScore;

        protected override string FeedBackMessage => "You get 1 Ball Back!";

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            _specialScore.Raise();
        }

        //Event Listener
        public void ChangePos()
        {
            int i = Random.Range(0,_scores.Length);
            SkeeBallScore iScore = _scores[i];
            transform.SetPositionAndRotation(iScore.transform.position, iScore.transform.rotation);
        }
    }
}  
