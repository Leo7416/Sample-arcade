using SampleArcade.Camera;
using SampleArcade.Enemy;
using SampleArcade.GameManagers;
using SampleArcade.CompositionRoot;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using SampleArcade.Timer;

namespace SampleArcade
{
    public class CharacterSpawner : MonoBehaviour
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

        private float _currentSpawnTimerSeconds;
        private float _nextSpawnIntervalSeconds;
        private int _currentPlayerCount;
        private int _currentEnemyCount;

        private static bool _isPlayerSpawnedInAnyZone = false;

        private GameManager _gameManager;
        private ITimer _timer;

        protected void Awake()
        {
            _nextSpawnIntervalSeconds = Random.Range(_minSpawnIntervalSeconds, _maxSpawnIntervalSeconds);
            _gameManager = FindObjectOfType<GameManager>();
            _timer = new UnityTimer();

            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                _isPlayerSpawnedInAnyZone = false;
            }
        }

        protected void Update()
        {
            _currentSpawnTimerSeconds += _timer.DeltaTime;

            if (_currentSpawnTimerSeconds >= _nextSpawnIntervalSeconds)
            {
                _currentSpawnTimerSeconds = 0f;

                if (_currentPlayerCount < _maxPlayerCount && !_isPlayerSpawnedInAnyZone)
                {
                    var player = SpawnCharacter(_playerCharacterCompositionRoot, ref _currentPlayerCount) as PlayerCharacterView;
                    if (player != null)
                    {
                        _isPlayerSpawnedInAnyZone = true;

                        var cameraController = FindObjectOfType<CameraController>();
                        if (cameraController != null)
                        {
                            cameraController.SetPlayer(player);
                        }

                        _gameManager.RegisterPlayer(player);
                    }
                }
                else if (_currentEnemyCount < _maxEnemyCount)
                {
                    var enemy = SpawnCharacter(_enemyCharacterCompositionRoot, ref _currentEnemyCount) as EnemyCharacterView;
                    _gameManager.RegisterEnemy(enemy);
                }

                _nextSpawnIntervalSeconds = Random.Range(_minSpawnIntervalSeconds, _maxSpawnIntervalSeconds);
            }
        }

        private BaseCharacterView SpawnCharacter(CharacterCompositionRoot characterCompositionRoot, ref int currentCount)
        {
            var randomPointInsideRange = Random.insideUnitCircle * _range;
            var randomPosition = new Vector3(randomPointInsideRange.x, 0f, randomPointInsideRange.y) + transform.position;

            var compositionRootInstance = Instantiate(characterCompositionRoot, randomPosition, Quaternion.identity, transform);
            var composedCharacter = compositionRootInstance.Compose(_timer);

            currentCount++;

            return composedCharacter;
        }

        protected void OnDrawGizmos()
        {
            var cachedColor = Handles.color;
            Handles.color = Color.blue;
            Handles.DrawWireDisc(transform.position, Vector3.up, _range);
            Handles.color = cachedColor;
        }
    }
}
