using UnityEngine;
using ScriptableObjectEvents;

namespace SkeeBall
{
    public class Ball : MonoBehaviour
    {
        [Header("Thorwn")]
        [SerializeField] private VoidEvent _thrownEvent;
        [SerializeField] private bool _thrown;

        [Header("Velocity")]
        [SerializeField] private Rigidbody _rb;
        [Tooltip("The event triggers when the rigidibody speed is under this value")]
        [SerializeField] private float _velocityTolerance = 1f;
        [SerializeField] private VoidEvent _stopMovingEvent;

        private void LateUpdate()
        {
            if (!_thrown)
                return;
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

        public void Thrown(Vector3 direction, float force)
        {
            transform.SetParent(null);
            _rb.isKinematic = false;
            //Add force takes 2 frames to add?????????????????????????????????
            _rb.velocity = direction * force;
            _rb.useGravity = true;
            _thrown = true;
            _thrownEvent.Raise();
        }

        public Ball PickUp()
        {
            return this;
        }
    }
}
