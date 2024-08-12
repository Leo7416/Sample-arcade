using SampleArcade.FSM;

namespace SampleArcade.Enemy.States
{
    internal class IdleState : BaseState
    {
        private readonly EnemySprintingController _enemySprintingController;

        public IdleState(EnemySprintingController enemySprintingController)
        {
            _enemySprintingController = enemySprintingController;
        }

        public override void Execute()
        {
            _enemySprintingController.IsSprinting = false;
        }
    }
}