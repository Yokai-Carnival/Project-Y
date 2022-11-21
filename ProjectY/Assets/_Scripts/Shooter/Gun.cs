using Unity.Burst.CompilerServices;
using UnityEngine;

namespace Shooter
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private float _range = 20;
        private Vector3 _hitPoint;
        [SerializeField] private Camera _cam;

        private void Update()
        {
            CheckInput();
        }

        private void CheckInput()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, _range))
            {
                if (hit.transform.TryGetComponent(out TargetScore targetable))
                {
                    targetable.ChangeManagerScore(hit.point);
                }
                _hitPoint = hit.point;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(_cam.transform.position, _hitPoint);
        }
    }
}
