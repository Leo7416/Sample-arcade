using UnityEngine;

namespace SampleArcade.Boosts
{
    public abstract class Boost : MonoBehaviour
    {
        protected BaseCharacterView _characterView;

        public abstract void ActivateBoost(BaseCharacterView character);

        protected abstract void DeactivateBoost();
    }
}