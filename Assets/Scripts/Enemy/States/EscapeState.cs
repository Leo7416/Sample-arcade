using SampleArcade.FSM;
using UnityEngine;

namespace SampleArcade.Enemy.States
{
    internal class EscapeState : BaseState
    {
        private readonly EnemyTarget _target;
        private readonly EnemyDirectionController _enemyDirectionController;
        private readonly EnemySprintingController _enemySprintingController;
        private readonly EnemyCharacter _enemyCharacter;

        private Vector3 _currentPoint;

        public EscapeState(EnemyTarget target, EnemyDirectionController enemyDirectionController,
            EnemySprintingController enemySprintingController, EnemyCharacter enemyCharacter)
        {
            _target = target;
            _enemyDirectionController = enemyDirectionController;
            _enemySprintingController = enemySprintingController;
            _enemyCharacter = enemyCharacter;
        }

        public override void Execute()
        {
            var targetPosition = _target.Closest.transform.position;
            var enemyPosition = _enemyDirectionController.transform.position;

            var directionToTarget = (targetPosition - enemyPosition).normalized;
            var escapeDirection = -directionToTarget;

            _currentPoint = enemyPosition + escapeDirection * _enemyCharacter.EscapeDistance;

            float distanceToTarget = Vector3.Distance(enemyPosition, targetPosition);

            if (distanceToTarget < _enemyCharacter.EscapeDistance)
            {
                _enemyDirectionController.UpdateMovementDirection(_currentPoint);
                _enemySprintingController.IsSprinting = true;
            }
            else
            {
                _enemySprintingController.IsSprinting = false;
            }
        }
    }
}
