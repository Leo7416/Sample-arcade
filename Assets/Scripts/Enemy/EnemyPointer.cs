using SampleArcade.GameManagers;
using System;
using UnityEngine;

namespace SampleArcade.Enemy
{
    public class EnemyPointer : MonoBehaviour
    {
        private BaseCharacter _сharacter;

        private void Start()
        {
            _сharacter = GetComponent<BaseCharacter>();

            if (_сharacter != null)
            {
                _сharacter.Dead += OnEnemyDead;
            }

            PointerManager.Instance.AddToList(this);
        }

        private void OnEnemyDead(BaseCharacter character)
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
