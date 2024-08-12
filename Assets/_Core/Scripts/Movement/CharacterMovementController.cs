using SampleArcade.Timer;
using UnityEngine;

namespace SampleArcade.Movement
{
    public class CharacterMovementController : IMovementController
    {
        private static readonly float SqrEpsilon = Mathf.Epsilon * Mathf.Epsilon;

        private readonly ITimer _timer;

        private readonly float _speed;
        private readonly float _maxRadiansDelta;
        private readonly float _sprint;

        private float _currentSpeed;

        public CharacterMovementController(ICharacterConfig config, ITimer timer)
        {
            _speed = config.Speed;
            _maxRadiansDelta = config.MaxRadiansDelta;
            _sprint = config.Sprint;

            _timer = timer;
            _currentSpeed = _speed;
        }

        public Vector3 Translate(Vector3 movementDirection)
        {
            return movementDirection * _currentSpeed * _timer.DeltaTime;
        }

        public Quaternion Rotate(Quaternion currentRotation, Vector3 lookDirection)
        {
            if (_maxRadiansDelta > 0f && lookDirection != Vector3.zero)
            {
                var currentLookDirection = currentRotation * Vector3.forward;
                float sqrMagnitude = (currentLookDirection - lookDirection).sqrMagnitude;

                if (sqrMagnitude > SqrEpsilon)
                {
                    var newRotation = Quaternion.Slerp(
                        currentRotation,
                        Quaternion.LookRotation(lookDirection, Vector3.up),
                        _maxRadiansDelta * _timer.DeltaTime);

                    return newRotation;
                }
            }
            return currentRotation;
        }

        public float SetSprint(bool isSprinting)
        {
            if (isSprinting)
            {
                _currentSpeed = _speed * _sprint;
                return _currentSpeed;
            }
            else
            {
                _currentSpeed = _speed;
                return _currentSpeed;
            }
        }

        public float MultiplySpeedBoost(float boostSpeed)
        {
            _currentSpeed = _speed * boostSpeed;
            return _currentSpeed;
        }
    }
}