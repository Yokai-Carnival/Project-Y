using UnityEngine;

namespace Shooter
{
    public class MouseGun : BaseGun
    {
        private bool _shoot;
        [SerializeField] private Camera _cam;
        protected override Ray Ray => _cam.ScreenPointToRay(Input.mousePosition);

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _shoot = true;
            }
        }

        private void FixedUpdate()
        {
            if(_shoot)
            {
                _shoot = false;
                Shoot();
            }
        }
    }
}
