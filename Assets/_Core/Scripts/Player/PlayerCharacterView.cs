using UnityEngine;

namespace SampleArcade.Player
{
#if UNITY_ANDROID
    [RequireComponent(typeof(JoystickPlayerMovementDirectionController), typeof(ButtonPlayerSprintingController))]
#else
    [RequireComponent(typeof(PlayerMovementDirectionController), typeof(PlayerSprintingController))]
#endif

    public class PlayerCharacterView : BaseCharacterView
    {

    }
}