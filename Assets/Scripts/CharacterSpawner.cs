using SampleArcade.Camera;
using UnityEditor;
using UnityEngine;

namespace SampleArcade
{
    public class CharacterSpawner : MonoBehaviour
    {
        [SerializeField]
        private BaseCharacter _playerCharacterPrefab;

        [SerializeField]
        private BaseCharacter _enemyCharacterPrefab;

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

        protected void Awake()
        {
            _nextSpawnIntervalSeconds = Random.Range(_minSpawnIntervalSeconds, _maxSpawnIntervalSeconds);
        }

        protected void Update()
        {
            _currentSpawnTimerSeconds += Time.deltaTime;

            if (_currentSpawnTimerSeconds >= _nextSpawnIntervalSeconds)
            {
                _currentSpawnTimerSeconds = 0f;

                if (_currentPlayerCount < _maxPlayerCount && !_isPlayerSpawnedInAnyZone)
                {
                    var player = SpawnCharacter(_playerCharacterPrefab, ref _currentPlayerCount) as PlayerCharacter;
                    if (player != null)
                    {
                        _isPlayerSpawnedInAnyZone = true;

                        var cameraController = FindObjectOfType<CameraController>();
                        if (cameraController != null)
                        {
                            cameraController.SetPlayer(player);
                        }
                    }
                }
                else if (_currentEnemyCount < _maxEnemyCount)
                {
                    SpawnCharacter(_enemyCharacterPrefab, ref _currentEnemyCount);
                }

                _nextSpawnIntervalSeconds = Random.Range(_minSpawnIntervalSeconds, _maxSpawnIntervalSeconds);
            }
        }

        private BaseCharacter SpawnCharacter(BaseCharacter characterPrefab, ref int currentCount)
        {
            var randomPointInsideRange = Random.insideUnitCircle * _range;
            var randomPosition = new Vector3(randomPointInsideRange.x, 0f, randomPointInsideRange.y) + transform.position;

            var character = Instantiate(characterPrefab, randomPosition, Quaternion.identity, transform);
            currentCount++;

            return character;
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
