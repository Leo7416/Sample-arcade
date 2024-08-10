using SampleArcade.Timer;
using UnityEngine;

namespace SampleArcade.Shooting
{
    public class BulletView : MonoBehaviour
    {
        private BulletModel _Model;

        private ITimer _timer;

        public float Damage { get { return _Model?.Damage ?? 0f; } }

        public void Awake()
        {
            _timer = new UnityTimer();
        }

        public void Initialize(float damage, Vector3 direction, float flySpeed, float maxFlyDistance)
        {
            _Model = new BulletModel(damage, direction, flySpeed, maxFlyDistance, _timer);
        }

        protected void Update()
        {
            if (_Model == null) return;

            var nextPosition = _Model.GetNextPosition();
            transform.Translate(nextPosition);

            if (_Model.BulletFlight())
            {
                Destroy(gameObject);
            }
        }
    }
}