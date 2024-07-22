using UnityEngine;

namespace SampleArcade.Boosts
{
    public class SpeedBoost : Boost
    {
        [SerializeField]
        public float _sprintMultiplier = 2f;
        [SerializeField]
        public float _durationSeconds = 5f;

        public override void ActivateBoost(BaseCharacter character)
        {
            if (Character)
            {
                CancelInvoke(nameof(DeactivateBoost));
                DeactivateBoost();
            }
            base.ActivateBoost(character);
            character.MultiplySpeed(_sprintMultiplier);
            Invoke(nameof(DeactivateBoost), _durationSeconds);
        }

        protected override void DeactivateBoost()
        {
            if (Character)
                Character.ResetSpeed();
            base.DeactivateBoost();
        }


    }
}
