using SampleArcade.Camera;
using SampleArcade.CompositionRoot;
using SampleArcade.Enemy;
using SampleArcade.GameManagers;
using SampleArcade.Player;
using SampleArcade.Timer;
using TMPro.Examples;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SampleArcade
{
    public class CharacterSpawnerView : MonoBehaviour
    {
        [SerializeField]
        private CharacterCompositionRoot _playerCharacterCompositionRoot;

        [SerializeField]
        private CharacterCompositionRoot _enemyCharacterCompositionRoot;

        [SerializeField]
        private float _range = 10f;

        [SerializeField]
        private int _maxPlayerCount = 1;

        [SerializeField]
        private int _maxEnemyCount = 5;

        [SerializeField]
        private float _minSpawnIntervalSeconds = 5f;

        [SerializeField]
        private float _maxSpawnIntervalSeconds = 15f;

        private CharacterSpawnerModel Model;
        private CameraControllerView _cameraController;

        private ITimer _timer;

        protected void Awake()
        {
            _cameraController = FindObjectOfType<CameraControllerView>();

            _timer = new UnityTimer();
            Model = new CharacterSpawnerModel(
                _playerCharacterCompositionRoot,
                _enemyCharacterCompositionRoot,
                _range,
                _maxPlayerCount,
                _maxEnemyCount,
                _minSpawnIntervalSeconds,
                _maxSpawnIntervalSeconds,
                _timer,
                GameManager.Instance
            );

            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                Model.ResetPlayerSpawnFlag();
            }
        }

        protected void Update()
        {
            Model.UpdateSpawnTimer(transform.position, out var spawnedCharacter, out var isPlayerSpawned);

            if (spawnedCharacter != null)
            {
                if (isPlayerSpawned && spawnedCharacter is PlayerCharacterView player)
                {
                    _cameraController.SetPlayer(player);
                    GameManager.Instance.RegisterPlayer(player);
                }
                else if (spawnedCharacter is EnemyCharacterView enemy)
                {
                    GameManager.Instance.RegisterEnemy(enemy);
                }
            }
        }

        protected void OnDrawGizmos()
        {
            var cachedColor = Handles.color;
            Handles.color = Color.blue;
            Handles.DrawWireDisc(transform.position, Vector3.up, Model?.Range ?? _range);
            Handles.color = cachedColor;
        }
    }
}
