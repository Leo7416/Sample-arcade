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

        public Vector3 MovementDirection { get; set; }
        public Vector3 LookDirection { get; set; }

        private CharacterController _characterController;

        private Boost _currentBoost;

        protected void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        protected void Update()
        {
            Translate();

            if (_maxRadiansDelta > 0f && LookDirection != Vector3.zero)
                Rotate();
        }

        private void Translate()
        {
            var currentSpeed = _speed;
            if (Input.GetKey(KeyCode.Space))
                currentSpeed *= _sprint;

            if (_currentBoost != null)
                currentSpeed *= _currentBoost.SprintMultiplier;

            var delta = MovementDirection * currentSpeed * Time.deltaTime;
            _characterController.Move(delta);
        }

        private void Rotate()
        {
            var currentLookDirection = transform.rotation * Vector3.forward;
            float sqrMagnitude = (currentLookDirection - LookDirection).sqrMagnitude;

            if (sqrMagnitude > SqrEpsilon)
            {
                var newRotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(LookDirection, Vector3.up),
                    _maxRadiansDelta * Time.deltaTime);

                transform.rotation = newRotation;
            }
        }

        public void ApplyBoost(Boost boost)
        {
            _currentBoost = boost;
            Invoke(nameof(ClearBoost), boost.Duration); 
        }

        private void ClearBoost()
        {
            _currentBoost = null;
        }
    }
}
