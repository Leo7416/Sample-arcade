using UnityEngine;

namespace SampleArcade.PickUp
{
    public class PickUpSpawnerModel
    {
        private readonly PickUpItem _pickUpPrefab;
        private readonly float _range;
        private readonly int _maxCount;
        private readonly float _minSpawnIntervalSeconds;
        private readonly float _maxSpawnIntervalSeconds;

        private float _currentSpawnTimerSeconds;
        private float _nextSpawnIntervalSeconds;
        private int _currentCount;

        public PickUpSpawnerModel(
            PickUpItem pickUpPrefab,
            float range,
            int maxCount,
            float minSpawnIntervalSeconds,
            float maxSpawnIntervalSeconds)
        {
            _pickUpPrefab = pickUpPrefab;
            _range = range;
            _maxCount = maxCount;
            _minSpawnIntervalSeconds = minSpawnIntervalSeconds;
            _maxSpawnIntervalSeconds = maxSpawnIntervalSeconds;

            _nextSpawnIntervalSeconds = Random.Range(_minSpawnIntervalSeconds, _maxSpawnIntervalSeconds);
        }

        public bool CanSpawn => _currentCount < _maxCount;

        public Vector3 GetRandomSpawnPosition(Vector3 spawnerPosition)
        {
            var randomPointInsideRange = Random.insideUnitCircle * _range;
            return new Vector3(randomPointInsideRange.x, 0f, randomPointInsideRange.y) + spawnerPosition;
        }

        public void RegisterSpawnedItem()
        {
            _currentCount++;
        }

        public void OnItemPickedUp()
        {
            _currentCount--;
        }

        public void ResetSpawnTimer()
        {
            _currentSpawnTimerSeconds = 0f;
            _nextSpawnIntervalSeconds = Random.Range(_minSpawnIntervalSeconds, _maxSpawnIntervalSeconds);
        }

        public bool ShouldSpawn(float deltaTime)
        {
            _currentSpawnTimerSeconds += deltaTime;
            return _currentSpawnTimerSeconds > _nextSpawnIntervalSeconds && CanSpawn;
        }

        public float Range => _range;
    }
}
