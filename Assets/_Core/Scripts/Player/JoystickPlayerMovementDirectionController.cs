#if UNITY_ANDROID
using SampleArcade.GameManagers;
using SampleArcade.Movement;
using UnityEngine;

namespace SampleArcade.Player
{
    public class JoystickPlayerMovementDirectionController : MonoBehaviour, IMovementDirectionSource
    {
        private UnityEngine.Camera _camera;
        private Joystick _joystick;

        public Vector3 MovementDirection { get; private set; }

        protected void Awake()
        {
            _camera = UnityEngine.Camera.main;
            _joystick = GameManager.Instance.Joystick;
        }

        protected void Update()
        {
            var horizontal = _joystick.Horizontal;
            var vertical = _joystick.Vertical;

            var direction = new Vector3 (horizontal, 0, vertical);
            direction = _camera.transform.rotation * direction;
            direction.y = 0;
            
            MovementDirection = direction.normalized;
        }
    }
}
#endif