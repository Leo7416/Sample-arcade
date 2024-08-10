using SampleArcade.GameManagers;
using SampleArcade.CompositionRoot;
using UnityEngine;
using SampleArcade.Timer;
using SampleArcade.Player;

namespace SampleArcade
{
    public class CharacterSpawnerModel
    {
        private readonly CharacterCompositionRoot _playerCharacterCompositionRoot;
        private readonly CharacterCompositionRoot _enemyCharacterCompositionRoot;
        private readonly float _range;
        private readonly int _maxPlayerCount;
        private readonly int _maxEnemyCount;
        private readonly float _minSpawnIntervalSeconds;
        private readonly float _maxSpawnIntervalSeconds;

        private float _currentSpawnTimerSeconds;
        private float _nextSpawnIntervalSeconds;
        private int _currentPlayerCount;
        private int _currentEnemyCount;
        private static bool _isPlayerSpawnedInAnyZone = false;

        private readonly ITimer _timer;
        private readonly GameManager _gameManager;

        public CharacterSpawnerModel(
            CharacterCompositionRoot playerCharacterCompositionRoot,
            CharacterCompositionRoot enemyCharacterCompositionRoot,
            float range, int maxPlayerCount,
            int maxEnemyCount, float minSpawnIntervalSeconds,
            float maxSpawnIntervalSeconds, ITimer timer,
            GameManager gameManager)
        {
            _playerCharacterCompositionRoot = playerCharacterCompositionRoot;
            _enemyCharacterCompositionRoot = enemyCharacterCompositionRoot;
            _range = range;
            _maxPlayerCount = maxPlayerCount;
            _maxEnemyCount = maxEnemyCount;
            _minSpawnIntervalSeconds = minSpawnIntervalSeconds;
            _maxSpawnIntervalSeconds = maxSpawnIntervalSeconds;
            _timer = timer;
            _gameManager = gameManager;

            _nextSpawnIntervalSeconds = Random.Range(_minSpawnIntervalSeconds, _maxSpawnIntervalSeconds);
        }

        public void UpdateSpawnTimer(Vector3 spawnerPosition, out BaseCharacterView spawnedCharacter, out bool isPlayerSpawned)
        {
            _currentSpawnTimerSeconds += _timer.DeltaTime;
            spawnedCharacter = null;
            isPlayerSpawned = false;

            if (_currentSpawnTimerSeconds >= _nextSpawnIntervalSeconds)
            {
                _currentSpawnTimerSeconds = 0f;

                if (_currentPlayerCount < _maxPlayerCount && !_isPlayerSpawnedInAnyZone)
                {
                    spawnedCharacter = SpawnCharacter(_playerCharacterCompositionRoot, ref _currentPlayerCount, spawnerPosition);
                    _isPlayerSpawnedInAnyZone = spawnedCharacter is PlayerCharacterView;
                    isPlayerSpawned = _isPlayerSpawnedInAnyZone;
                }
                else if (_currentEnemyCount < _maxEnemyCount)
                {
                    spawnedCharacter = SpawnCharacter(_enemyCharacterCompositionRoot, ref _currentEnemyCount, spawnerPosition);
                }

                _nextSpawnIntervalSeconds = Random.Range(_minSpawnIntervalSeconds, _maxSpawnIntervalSeconds);
            }
        }

        private BaseCharacterView SpawnCharacter(CharacterCompositionRoot characterCompositionRoot, ref int currentCount, Vector3 spawnerPosition)
        {
            var randomPointInsideRange = Random.insideUnitCircle * _range;
            var randomPosition = new Vector3(randomPointInsideRange.x, 0f, randomPointInsideRange.y) + spawnerPosition;

            var compositionRootInstance = Object.Instantiate(characterCompositionRoot, randomPosition, Quaternion.identity);
            var composedCharacter = compositionRootInstance.Compose(_timer);

            currentCount++;

            return composedCharacter;
        }

        public void ResetPlayerSpawnFlag()
        {
            _isPlayerSpawnedInAnyZone = false;
        }

        public float Range => _range;
    }
}
