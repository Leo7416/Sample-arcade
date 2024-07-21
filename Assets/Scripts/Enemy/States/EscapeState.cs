using SampleArcade.FSM;
using UnityEngine;

namespace SampleArcade.Enemy.States
{
    internal class EscapeState : BaseState
    {
        private readonly EnemyTarget _target;
        private readonly EnemyDirectionController _enemyDirectionController;
        private readonly Transform _enemyTransform;
        private readonly float _escapeDistance;
        private readonly float _escapeProbability;
        private readonly System.Random _random;

        private Vector3 _currentPoint;

        public EscapeState(EnemyTarget target, EnemyDirectionController enemyDirectionController, Transform enemyTransform, float escapeDistance, float escapeProbability, System.Random random)
        {
            _target = target;
            _enemyDirectionController = enemyDirectionController;
            _enemyTransform = enemyTransform;
            _escapeDistance = escapeDistance;
            _escapeProbability = escapeProbability;
            _random = random;
        }

        public override void Execute()
        {
            Vector3 targetPosition = _target.Closest.transform.position;
            Vector3 directionAwayFromTarget = (_enemyTransform.position - targetPosition).normalized;
            Vector3 runAwayPosition = targetPosition + directionAwayFromTarget * _escapeDistance;

            if (_currentPoint != targetPosition && _random.NextDouble() < _escapeProbability)
            {
                _currentPoint = targetPosition;
                _enemyDirectionController.UpdateMovementDirection(runAwayPosition);
            }
        }
    }
}
