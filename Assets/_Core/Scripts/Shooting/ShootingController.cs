using SampleArcade.Timer;
using UnityEngine;

namespace SampleArcade.Shooting
{
    public class ShootingController
    {
        public bool HasTarget => _target != null;
        public Vector3 TargetPosition => _target.Transform.Position;
        public WeaponModel Weapon => _weapon;

        private readonly IShootingTarget _shootingTarget;
        private readonly ITimer _timer;

        private WeaponModel _weapon;
        private BaseCharacterModel _target;

        private float _nextShotTimeSec;

        public ShootingController(IShootingTarget shootingTarget, ITimer timer)
        {
            _shootingTarget = shootingTarget;
            _timer = timer;
        }

        public void TryShoot(Vector3 position)
        {
            _target = _shootingTarget.GetTarget(position, _weapon.Description.ShootRadius);

            _nextShotTimeSec -= _timer.DeltaTime;
            if (_nextShotTimeSec < 0 )
            {
                if (HasTarget)
                    _weapon.Shoot(position, TargetPosition);

                _nextShotTimeSec = _weapon.Description.ShootFrequencySec;
            }
        }

        public void SetWeapon(WeaponModel weapon)
        {
            _weapon = weapon;
        }
    }
}