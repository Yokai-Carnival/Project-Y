using UnityEngine;
using ScriptableObjectEvents;

namespace SkeeBall
{
    public class Ball : MonoBehaviour
    {
        [Header("Thorwn")]
        [SerializeField] private VoidEvent _thrownEvent;

        [Header("Velocity")]
        [SerializeField] private Rigidbody _rb;
        [Tooltip("The event triggers when the rigidibody velocity magnitude is under this value")]
        [SerializeField] private float _velocityTolerance = 1f;
        [SerializeField] private VoidEvent _stopMovingEvent;

        private void Update()
        {
            if(CheckIfMoving())
            {
                _stopMovingEvent.Raise();
                Destroy(gameObject);
            }
        }

        private bool CheckIfMoving()
        {
            return _rb.velocity.magnitude < _velocityTolerance;
        }

        public void Thorwn()
        {
            _thrownEvent.Raise();
        }
    }
}
