using SampleArcade.Enemy.States;
using UnityEngine;

namespace SampleArcade.Enemy
{
    public class EnemyAIController : MonoBehaviour
    {
        [SerializeField]
        private float _viewRadius = 20f;
        [SerializeField]
        private float _escapeDistance = 4f;
        [SerializeField]
        private float _escapeProbability = 0.7f;

        private EnemyTarget _target;
        private EnemyStateMachine _stateMachine;

        protected void Awake()
        {
            var player = FindObjectOfType<PlayerCharacter>();

            var enemyDirectionController = GetComponent<EnemyDirectionController>();

            var character = GetComponent<BaseCharacter>();

            var navMesher = new NavMesher(transform);
            _target = new EnemyTarget(transform, player, _viewRadius, character);

            var random = new System.Random();

            _stateMachine = new EnemyStateMachine(enemyDirectionController, navMesher, _target, transform, character, _escapeDistance, _escapeProbability, random);
        }

        protected void Update()
        {
            _target.FindClosest();
            _stateMachine.Update();
        }
    }
}
