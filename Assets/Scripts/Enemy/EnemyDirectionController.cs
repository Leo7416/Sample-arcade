using SampleArcade.Movement;
using UnityEngine;

namespace SampleArcade.Enemy
{
    public class EnemyDirectionController : MonoBehaviour, IMovementDirectionSource
    {
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
            MovementDirection = new Vector3(realDirection.x, 0, realDirection.z).normalized;
        }
    }
}
