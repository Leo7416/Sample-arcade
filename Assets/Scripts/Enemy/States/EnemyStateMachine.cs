using SampleArcade.FSM;
using System.Collections.Generic;
using UnityEngine;

namespace SampleArcade.Enemy.States
{
    public class EnemyStateMachine : BaseStateMachine
    {
        private const float NavMeshTurnOffDistance = 5f;

        public EnemyStateMachine(EnemyDirectionController enemyDirectionController,
            NavMesher navMesher, EnemyTarget target, Transform enemyTransform, BaseCharacter character, 
            float escapeDistance, float escapeProbability, System.Random random)
        {
            var idleState = new IdleState();
            var findWayState = new FindWayState(target, navMesher, enemyDirectionController);
            var moveForwardState = new MoveForwardState(target, enemyDirectionController);
            var escapeState = new EscapeState(target, enemyDirectionController, enemyTransform,
                escapeDistance, escapeProbability, random);

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
                    () => character.Health <= character.LowHealthThreshold)
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
                    () => character.Health <= character.LowHealthThreshold)
            });

            AddState(state: escapeState, transitions: new List<Transition>
            {
                new Transition(
                    idleState,
                    () => target.Closest == null)
            });
        }
    }
}
