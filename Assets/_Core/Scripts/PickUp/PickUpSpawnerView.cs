using SampleArcade.Timer;
using UnityEditor;
using UnityEngine;

namespace SampleArcade.PickUp
{
    public class PickUpSpawnerView : MonoBehaviour
    {
        [SerializeField]
        private PickUpItem _pickUpPrefab;

        [SerializeField]
        private float _range = 2f;

        [SerializeField]
        private int _maxCount = 2;

        [SerializeField]
        private float _minSpawnIntervalSeconds = 5f;

        [SerializeField]
        private float _maxSpawnIntervalSeconds = 15f;

        private PickUpSpawnerModel Model;

        private ITimer _timer;

        protected void Awake()
        {
            _timer = new UnityTimer();

            Model = new PickUpSpawnerModel(
                _pickUpPrefab,
                _range,
                _maxCount,
                _minSpawnIntervalSeconds,
                _maxSpawnIntervalSeconds
            );
        }

        protected void Update()
        {
            if (Model.ShouldSpawn(_timer.DeltaTime))
            {
                var randomPosition = Model.GetRandomSpawnPosition(transform.position);
                var spawnedItem = Instantiate(_pickUpPrefab, randomPosition, Quaternion.identity, transform);

                if (spawnedItem != null)
                {
                    Model.RegisterSpawnedItem();
                    spawnedItem.OnPickedUp += OnItemPickedUp;
                }

                Model.ResetSpawnTimer();
            }
        }

        private void OnItemPickedUp(PickUpItem pickedUpItem)
        {
            Model.OnItemPickedUp();
            pickedUpItem.OnPickedUp -= OnItemPickedUp;
        }

        protected void OnDrawGizmos()
        {
            var cashedColor = Handles.color;
            Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position, Vector3.up, Model?.Range ?? _range);
            Handles.color = cashedColor;
        }
    }
}