using UnityEngine;

namespace SampleArcade.UI
{
    public class HealthBarUIModel
    {
        private float _healthPercent;

        public float HealthPercent
        {
            get => _healthPercent;
            private set => _healthPercent = value;
        }

        public HealthBarUIModel(float Health)
        {
            _healthPercent = Health;
        }

        public void UpdateHealth(float newHealthPercent)
        {
            HealthPercent = newHealthPercent;
        }
    }
}