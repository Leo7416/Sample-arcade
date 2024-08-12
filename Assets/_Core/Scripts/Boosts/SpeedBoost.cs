using UnityEngine;

namespace SampleArcade.Boosts
{
    public class SpeedBoost : Boost
    {
        [SerializeField]
        private float _sprintMultiplier = 2f;
        [SerializeField]
        private float _durationSeconds = 5f;

        public override void ActivateBoost(BaseCharacterView characterView)
        {
            characterView.ActivateSpeedBoostEffect(_sprintMultiplier, _durationSeconds);
            Invoke(nameof(DeactivateBoost), _durationSeconds);
        }

        protected override void DeactivateBoost()
        {
            if (_characterView != null)
            {
                _characterView.DeactivateSpeedBoostEffect();
                _characterView = null;
            }
        }
    }
}