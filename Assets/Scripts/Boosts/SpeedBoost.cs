using UnityEngine;

namespace SampleArcade.Boosts
{
    public class SpeedBoost : Boost
    {
        public bool HasBoost = false;

        [SerializeField]
        private float _sprintMultiplier = 2f;
        [SerializeField]
        private float _durationSeconds = 5f;

        public override void ActivateBoost(BaseCharacter character)
        {
            if (Character)
            {
                CancelInvoke(nameof(DeactivateBoost));
                DeactivateBoost();
            }

            base.ActivateBoost(character);
            character.MultiplySpeed(_sprintMultiplier);
            HasBoost = true;
            Invoke(nameof(DeactivateBoost), _durationSeconds);
        }

        protected override void DeactivateBoost()
        {
            if (Character)
            {
                Character.ResetSpeed();
                Character = null;
            }
            base.DeactivateBoost();
            HasBoost = false;
        }
    }
}
