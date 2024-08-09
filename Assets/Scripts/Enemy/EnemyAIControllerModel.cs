using SampleArcade.Enemy.States;

namespace SampleArcade.Enemy
{
    public class EnemyAIControllerModel
    {
        private EnemyStateMachine _stateMachine;
        private EnemyTarget _target;

        public EnemyAIControllerModel(EnemyCharacterView enemy, EnemyDirectionController directionController,
                            EnemySprintingController sprintingController, NavMesher navMesher, float viewRadius)
        {
            _target = new EnemyTarget(enemy, null, viewRadius);
            _stateMachine = new EnemyStateMachine(enemy, directionController, sprintingController, navMesher, _target);
        }

        public void UpdateAI(PlayerCharacterView player)
        {
            if (_target.Player == null)
                _target.Player = player;

            _target.FindClosest();
            _stateMachine.Update();
        }
    }
}
