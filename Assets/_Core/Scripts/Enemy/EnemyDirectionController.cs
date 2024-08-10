using SampleArcade.Movement;
using UnityEngine;

namespace SampleArcade.Enemy
{
    public class EnemyDirectionController : MonoBehaviour, IMovementDirectionSource
    {
        private BaseCharacterView _baseCharacter;
        public Vector3 MovementDirection { get; private set; }

        private void Awake()
        {
            _baseCharacter = GetComponent<BaseCharacterView>();
        }

        public void UpdateMovementDirection(Vector3 targetPosition)
        {
            var realDirection = targetPosition - transform.position;
            MovementDirection = new Vector3(realDirection.x, 0, realDirection.z).normalized;
        }
    }
}
