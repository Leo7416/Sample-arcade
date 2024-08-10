using SampleArcade.Enemy;
using SampleArcade.Player;
using System.Collections.Generic;
using UnityEngine;

namespace SampleArcade.GameManagers
{
    public class PointerManager : MonoBehaviour
    {
        [SerializeField]
        PointerIcon _pointerPrefab;
        [SerializeField]
        UnityEngine.Camera _camera;

        private Transform _playerTransform;
        private PlayerCharacterView _player;

        private Dictionary<EnemyPointer, PointerIcon> _dictionary = new Dictionary<EnemyPointer, PointerIcon>();

        public static PointerManager Instance;

        protected void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public void AddToList(EnemyPointer enemyPointer)
        {
            if (!_dictionary.ContainsKey(enemyPointer))
            {
                PointerIcon newPointer = Instantiate(_pointerPrefab, transform);
                _dictionary.Add(enemyPointer, newPointer);
            }
        }

        public void RemoveFromList(EnemyPointer enemyPointer)
        {
            if (_dictionary.TryGetValue(enemyPointer, out PointerIcon pointerIcon))
            {
                Destroy(pointerIcon.gameObject);
                _dictionary.Remove(enemyPointer);
            }
        }

        protected void LateUpdate()
        {
            if (GameManager.Instance.Player == null) return;
            _playerTransform = GameManager.Instance.Player.transform;
            
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);

            List<EnemyPointer> keysToRemove = new List<EnemyPointer>();

            foreach (var kvp in _dictionary)
            {
                EnemyPointer enemyPointer = kvp.Key;
                PointerIcon pointerIcon = kvp.Value;

                if (enemyPointer == null)
                {
                    keysToRemove.Add(kvp.Key);
                    continue;
                }

                Vector3 toEnemy = enemyPointer.transform.position - _playerTransform.position;
                Ray ray = new Ray(_playerTransform.position, toEnemy);
                Debug.DrawRay(_playerTransform.position, toEnemy);

                float rayMinDistance = Mathf.Infinity;
                int index = 0;

                for (int p = 0; p < 4; p++)
                {
                    if (planes[p].Raycast(ray, out float distance))
                    {
                        if (distance < rayMinDistance)
                        {
                            rayMinDistance = distance;
                            index = p;
                        }
                    }
                }

                rayMinDistance = Mathf.Clamp(rayMinDistance, 0, toEnemy.magnitude);
                Vector3 worldPosition = ray.GetPoint(rayMinDistance);
                Vector3 position = _camera.WorldToScreenPoint(worldPosition);
                Quaternion rotation = GetIconRotation(index);

                if (toEnemy.magnitude > rayMinDistance)
                {
                    pointerIcon.Show();
                }
                else
                {
                    pointerIcon.Hide();
                }

                pointerIcon.SetIconPosition(position, rotation);
            }

            foreach (var key in keysToRemove)
            {
                RemoveFromList(key);
            }
        }

        private Quaternion GetIconRotation(int planeIndex)
        {
            switch (planeIndex)
            {
                case 0: return Quaternion.Euler(0f, 0f, 90f);
                case 1: return Quaternion.Euler(0f, 0f, -90f);
                case 2: return Quaternion.Euler(0f, 0f, 180f);
                case 3: return Quaternion.Euler(0f, 0f, 0f);
                default: return Quaternion.identity;
            }
        }
    }
}