﻿using SampleArcade.Enemy;
using System.Collections.Generic;
using UnityEngine;

namespace SampleArcade.GameManagers
{
    public class PointerManager : MonoBehaviour
    {
        [SerializeField]
        PointerIcon _pointerPrefab;
        [SerializeField]
        Transform _playerTransform;
        [SerializeField]
        UnityEngine.Camera _camera;

        private Dictionary<EnemyPointer, PointerIcon> _dictionary = new Dictionary<EnemyPointer, PointerIcon>();

        public static PointerManager Instance;
        private void Awake()
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
            PointerIcon newPointer = Instantiate(_pointerPrefab, transform);
            _dictionary.Add(enemyPointer, newPointer);
            enemyPointer.OnDestroyed += HandleEnemyDestroyed;
        }

        public void RemoveFromList(EnemyPointer enemyPointer)
        {
            if (_dictionary.TryGetValue(enemyPointer, out PointerIcon pointerIcon))
            {
                Destroy(pointerIcon.gameObject);
                _dictionary.Remove(enemyPointer);
                enemyPointer.OnDestroyed -= HandleEnemyDestroyed;
            }
        }

        private void HandleEnemyDestroyed(EnemyPointer enemyPointer)
        {
            RemoveFromList(enemyPointer);
        }

        void LateUpdate()
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);

            foreach (var kvp in _dictionary)
            {
                EnemyPointer enemyPointer = kvp.Key;
                PointerIcon pointerIcon = kvp.Value;

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

                pointerIcon.SetIconPosition(position, rotation, toEnemy.magnitude);
            }
        }

        Quaternion GetIconRotation(int planeIndex)
        {
            if (planeIndex == 0)
            {
                return Quaternion.Euler(0f, 0f, 90f);
            }
            else if (planeIndex == 1)
            {
                return Quaternion.Euler(0f, 0f, -90f);
            }
            else if (planeIndex == 2)
            {
                return Quaternion.Euler(0f, 0f, 180);
            }
            else if (planeIndex == 3)
            {
                return Quaternion.Euler(0f, 0f, 0f);
            }
            return Quaternion.identity;
        }
    }
}