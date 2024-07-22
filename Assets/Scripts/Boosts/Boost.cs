using SampleArcade;
using UnityEngine;

namespace SampleArcade.Boosts
{
    public abstract class Boost : MonoBehaviour
    {
        protected BaseCharacter Character;

        public virtual void ActivateBoost(BaseCharacter character)
        {
            Character = character;
        }

        protected virtual void DeactivateBoost()
        {
            Character = null;
        }
    }
}