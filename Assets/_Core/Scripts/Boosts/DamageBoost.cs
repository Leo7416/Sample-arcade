using UnityEngine;

namespace SampleArcade.Boosts
{
    public class DamageBoost : Boost
    {
        [SerializeField]
        private float _damageMultiplier = 1.5f;
        [SerializeField]
        private float _durationSeconds = 5f;

        public override void ActivateBoost(BaseCharacterView characterView)
        {
            characterView.ActivateDamageBoostEffect(_damageMultiplier, _durationSeconds);
            Invoke(nameof(DeactivateBoost), _durationSeconds);
        }

        protected override void DeactivateBoost()
        {
            if (_characterView != null)
            {
                _characterView.DeactivateDamageBoostEffect();
                _characterView = null;
            }
        }
    }
}