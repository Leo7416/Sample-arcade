using SampleArcade.GameManagers;
using SampleArcade.Player;
using UnityEngine;
namespace SampleArcade.Enemy
{
    public class EnemyAIControllerView : MonoBehaviour
    {
        [SerializeField]
        private float _viewRadius = 20f;

        private EnemyAIControllerModel _Model;

        protected void Awake()
        {
            var player = GetComponent<PlayerCharacterView>();
            var enemy = GetComponent<EnemyCharacterView>();
            var enemyDirectionController = GetComponent<EnemyDirectionController>();
            var enemySprintingController = GetComponent<EnemySprintingController>();
            var navMesher = new NavMesher(transform);

            _Model = new EnemyAIControllerModel(enemy, enemyDirectionController, enemySprintingController, navMesher, _viewRadius);
        }

        protected void Update()
        {
            var player = GameManager.Instance.Player;
            _Model.UpdateAI(player);
        }
    }
}