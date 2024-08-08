using SampleArcade.GameManagers;
using System;
using UnityEngine;

namespace SampleArcade.Enemy
{
    public class EnemyPointer : MonoBehaviour
    {
        private BaseCharacterView _сharacter;

        private void Start()
        {
            _сharacter = GetComponent<BaseCharacterView>();

            if (_сharacter != null)
            {
                _сharacter.Dead += OnEnemyDead;
            }

            PointerManager.Instance.AddToList(this);
        }

        private void OnEnemyDead(BaseCharacterView character)
        {
            if (_сharacter != null)
            {
                _сharacter.Dead -= OnEnemyDead;
            }
        }

        private void Destroy()
        {
            PointerManager.Instance.RemoveFromList(this);
        }
    }
}
