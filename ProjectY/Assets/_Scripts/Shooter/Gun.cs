using Unity.Burst.CompilerServices;
using UnityEngine;

namespace Shooter
{
    public class Gun : BaseGun
    {
        [SerializeField] private Camera _cam;
        protected override Ray Ray => _cam.ScreenPointToRay(Input.mousePosition);

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
    }
}
