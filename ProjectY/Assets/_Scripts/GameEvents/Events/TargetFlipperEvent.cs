using UnityEngine;
using Shooter;

namespace ScriptableObjectEvents
{
    [CreateAssetMenu(fileName = "New Target Flipper Event", menuName = "Game Events/Target Event")]
    public class TargetFlipperEvent : BaseGameEvent<TargetFlipper> { }
}
