using SampleArcade.Movement;
using UnityEngine;

namespace SampleArcade.Enemy
{
    public class EnemyDirectionController : MonoBehaviour, IMovementDirectionSource
    {
        [SerializeField]
        private float _sprintEnemy = 2f;

        private CharacterMovementController _movementController;
        private BaseCharacter _baseCharacter;
        public Vector3 MovementDirection { get; private set; }

        private void Awake()
        {
            _movementController = GetComponent<CharacterMovementController>();
            _baseCharacter = GetComponent<BaseCharacter>();
        }

        public void UpdateMovementDirection(Vector3 targetPosition)
        {
            var realDirection = targetPosition - transform.position;
            var normalizedDirection = new Vector3(realDirection.x, 0, realDirection.z).normalized;

            if (_baseCharacter.Health <= _baseCharacter.LowHealthThreshold)
                MovementDirection = normalizedDirection * _sprintEnemy;
            else
                MovementDirection = normalizedDirection;
        }
    }
}
