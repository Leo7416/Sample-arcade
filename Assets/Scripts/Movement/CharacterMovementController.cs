using SampleArcade.Boosts;
using UnityEngine;

namespace SampleArcade.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovementController : MonoBehaviour
    {
        private static readonly float SqrEpsilon = Mathf.Epsilon * Mathf.Epsilon;

        [SerializeField]
        private float _speed = 1f;
        [SerializeField]
        private float _maxRadiansDelta = 10f;
        [SerializeField]
        private float _sprint = 2f;

        private bool _isAlive = true;
        private float _boostSpeed;
        private float _currentSpeed;

        public Vector3 MovementDirection { get; set; }
        public Vector3 LookDirection { get; set; }

        private CharacterController _characterController;

        protected void Awake()
        {
            _characterController = GetComponent<CharacterController>();

            _boostSpeed = _speed;
        }

        protected void Update()
        {
            if (_isAlive)
            {
                Translate();

                if (_maxRadiansDelta > 0f && LookDirection != Vector3.zero)
                    Rotate();
            }
        }

        private void Translate()
        {
            var delta = MovementDirection * _currentSpeed * Time.deltaTime;
            _characterController.Move(delta);
        }

        private void Rotate()
        {
            var currentLookDirection = transform.rotation * Vector3.forward;
            float sqrMagnitude = (currentLookDirection - LookDirection).sqrMagnitude;

            if (sqrMagnitude <= SqrEpsilon) return;
            var newRotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(LookDirection, Vector3.up),
                _maxRadiansDelta * Time.deltaTime);

            transform.rotation = newRotation;
        }

        public void MultiplySpeed(float boost)
        {
            _boostSpeed *= boost;
        }

        public void SetSprint(bool isSprinting)
        {
            if (isSprinting)
                _currentSpeed = _boostSpeed * _sprint;
            else
                _currentSpeed = _boostSpeed;
        }

        public void ResetSpeed()
        {
            _boostSpeed = _speed;
        }
        public void SetAlive(bool isAlive)
        {
            _isAlive = isAlive;
        }
    }
}
