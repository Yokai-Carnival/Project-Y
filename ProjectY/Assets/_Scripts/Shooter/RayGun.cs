using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Shooter
{
    public class RayGun : BaseGun
    {
        protected override Ray Ray =>new(transform.position, transform.forward);

        private void Start()
        {
            XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
            grabbable.activated.AddListener(_ => Shoot());
            print(grabbable);
        }
    }
}
