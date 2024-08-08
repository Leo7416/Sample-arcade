using SampleArcade.Timer;
using UnityEngine;

namespace SampleArcade.Shooting
{
    public class BulletModel
    {
        public float Damage { get; private set; }

        private Vector3 _direction;
        private float _flySpeed;
        private float _maxFlyDistance;
        private float _currentFlyDistance;

        private ITimer _timer;

        public BulletModel(float damage, Vector3 direction, float flySpeed, 
            float maxFlyDistance, ITimer timer)
        {
            Damage = damage;
            _direction = direction;
            _flySpeed = flySpeed;
            _maxFlyDistance = maxFlyDistance;
            _timer = timer;
        }

        public bool BulletFlight()
        {
            var delta = _flySpeed * _timer.DeltaTime;
            _currentFlyDistance += delta;

            return _currentFlyDistance >= _maxFlyDistance;
        }
        
        public Vector3 GetNextPosition()
        {
            return _direction * (_flySpeed * _timer.DeltaTime);
        }
    }
}