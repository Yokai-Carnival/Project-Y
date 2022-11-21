using UnityEngine;
using System.Collections;
using ScriptableObjectEvents;

namespace Shooter
{
    public class TargetFlipper : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 100;

        [SerializeField] private TargetFlipperEvent _event;

        [Header("Score")]
        [SerializeField] private TargetScore _targetScore;
        [SerializeField] private float _shotRotationMultiplier = 5;
        [SerializeField] private TargetType _badOrGood = TargetType.Bad;
        public TargetType Type => _badOrGood;

        //Bad hardCoded but yeah
        private readonly Quaternion _laying = Quaternion.Euler(100, 0, 0);
        private readonly Quaternion _standing = Quaternion.Euler(0, 0, 0);

        private readonly WaitForEndOfFrame _wait = new();

        private void OnEnable() => _targetScore.Shot += ReturnToPool;

        private void OnDisable() => _targetScore.Shot -= ReturnToPool;

        public void Flip()
        {
            StopAllCoroutines();
            StartCoroutine(FlipCoroutine(_standing, -1));
        }

        public void FlipBack(float speedMultiplier = 1)
        {
            StopAllCoroutines();
            StartCoroutine(FlipCoroutine(_laying, speedMultiplier));
        }

        private IEnumerator FlipCoroutine(Quaternion rotation, float speedMultiplier = 1 ,int positive = 1)
        {
            while (!MathHelper.CompareRotations(transform.rotation, rotation, 10))
            {
                transform.Rotate(_rotationSpeed * positive * speedMultiplier * Time.deltaTime * Vector3.right);
                yield return _wait;
            }
            transform.rotation = rotation;
        }

        public void ReturnToPool()
        {
            FlipBack(_shotRotationMultiplier);
            _event.Raise(this);
        }

        public void SetType(TargetType type)
        {
            _badOrGood = type;
            if (type == TargetType.Good)
                _targetScore.SetScore(-_targetScore.Score);
            else
                _targetScore.SetScore(Mathf.Abs(_targetScore.Score));
        }
    }
}