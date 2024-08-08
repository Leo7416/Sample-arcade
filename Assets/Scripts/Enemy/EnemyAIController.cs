using SampleArcade.Enemy.States;
using UnityEngine;
namespace SampleArcade.Enemy
{
    public class EnemyAIController : MonoBehaviour
    {
        [SerializeField]
        private float _viewRadius = 20f;

        private EnemyTarget _target;
        private EnemyStateMachine _stateMachine;

        protected void Awake()
        {
            var player = FindObjectOfType<PlayerCharacterView>();

            var enemy = FindObjectOfType<EnemyCharacterView>();

            var enemyDirectionController = GetComponent<EnemyDirectionController>();

            var enemySprintingController = GetComponent<EnemySprintingController>();

            var navMesher = new NavMesher(transform);

            _target = new EnemyTarget(enemy, player, _viewRadius);

            _stateMachine = new EnemyStateMachine(enemy, enemyDirectionController,
                enemySprintingController, navMesher, _target);
        }

        protected void Update()
        {
            if (!_target.Player)
                _target.Player = FindObjectOfType<PlayerCharacterView>();
            _target.FindClosest();
            _stateMachine.Update();
        }
    }
}
