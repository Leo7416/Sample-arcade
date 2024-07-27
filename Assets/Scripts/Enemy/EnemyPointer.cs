using SampleArcade.GameManagers;
using System;
using UnityEngine;

namespace SampleArcade.Enemy
{
    public class EnemyPointer : MonoBehaviour
    {
        public event Action<EnemyPointer> OnDestroyed;

        private void Start()
        {
            PointerManager.Instance.AddToList(this);
        }

        private void OnDestroy()
        {
            PointerManager.Instance.RemoveFromList(this);
        }
    }
}
