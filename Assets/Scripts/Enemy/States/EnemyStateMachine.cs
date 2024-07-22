using SampleArcade.FSM;
using System.Collections.Generic;

namespace SampleArcade.Enemy.States
{
    public class EnemyStateMachine : BaseStateMachine
    {
        private const float NavMeshTurnOffDistance = 5f;

        public EnemyStateMachine(EnemyCharacter enemy, EnemyDirectionController enemyDirectionController,
             EnemySprintingController enemySprintingController,
             NavMesher navMesher, EnemyTarget target)
        {
            var idleState = new IdleState(enemySprintingController);
            var findWayState = new FindWayState(target, navMesher, enemyDirectionController);
            var moveForwardState = new MoveForwardState(target, enemyDirectionController);
            var escapeState = new EscapeState(target, enemyDirectionController,
                enemySprintingController);

            SetInitialState(idleState);

            AddState(state: idleState, transitions: new List<Transition>
            {
                new Transition(
                    findWayState,
                    () => target.DistanceClosestFromAgent() > NavMeshTurnOffDistance),
                new Transition(
                    moveForwardState,
                    () => target.DistanceClosestFromAgent() <= NavMeshTurnOffDistance),
                new Transition(
                    escapeState,
                    () => enemy.GetHealthPercent() <= enemy.EscapeHealthPercent &&
                          enemy.DecidesToRun() && target.IsTargetCharacter())
            });

            AddState(state: findWayState, transitions: new List<Transition>
            {
                new Transition(
                    idleState,
                    () => target.Closest == null),
                new Transition(
                    moveForwardState,
                    () => target.DistanceClosestFromAgent() <= NavMeshTurnOffDistance)
            });

            AddState(state: moveForwardState, transitions: new List<Transition>
            {
                new Transition(
                    idleState,
                    () => target.Closest == null),
                new Transition(
                    findWayState,
                    () => target.DistanceClosestFromAgent() > NavMeshTurnOffDistance),
                new Transition(
                    escapeState,
                    () => enemy.GetHealthPercent() <= enemy.EscapeHealthPercent &&
                          enemy.DecidesToRun() && target.IsTargetCharacter())
            });

            AddState(state: escapeState, transitions: new List<Transition>
            {
                new Transition(
                    idleState,
                    () => !target.IsTargetCharacter())
            });
        }
    }
}
