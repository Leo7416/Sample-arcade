using SampleArcade.GameManagers;
using UnityEngine;

namespace SampleArcade.Enemy
{
    public class EnemyPointer : MonoBehaviour
    {
        private BaseCharacterView _character;

        private void Start()
        {
            _character = GetComponent<BaseCharacterView>();

            if (_character != null)
            {
                _character.Dead += OnEnemyDead;
            }

            PointerManager.Instance.AddToList(this);
        }

        private void OnEnemyDead(BaseCharacterView character)
        {
            PointerManager.Instance.RemoveFromList(this);
        }

        private void OnDestroy()
        {
            
            if (_character != null)
            {
                _character.Dead -= OnEnemyDead;
            }
        }
    }
}