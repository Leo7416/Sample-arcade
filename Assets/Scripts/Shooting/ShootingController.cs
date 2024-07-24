using UnityEngine;

namespace SampleArcade.Shooting
{
    public class ShootingController : MonoBehaviour
    {
        public bool HasTarget => _target != null;
        public Vector3 TargetPosition => _target.transform.position; 

        private Weapon _weapon;

        private Collider[] _colliders = new Collider[2];
        private float _nextShotTimeSec;
        private GameObject _target;
        private bool _isAlive = true;

        protected void Update()
        {
            if (!_isAlive) return;

            _target = GetTarget();

            _nextShotTimeSec -= Time.deltaTime;
            if (_nextShotTimeSec < 0 )
            {
                if (HasTarget)
                    _weapon.Shoot(TargetPosition);

                _nextShotTimeSec = _weapon.ShootFrequencySec;
            }
        }

        public void SetAlive(bool isAlive)
        {
            _isAlive = isAlive;
        }

        public void SetWeapon(Weapon weaponPrefab, Transform hand)
        {
            if(_weapon != null)
                Destroy(_weapon.gameObject);
            
            _weapon = Instantiate(weaponPrefab, hand);
            _weapon.transform.localPosition = Vector3.zero;
            _weapon.transform.localRotation = Quaternion.identity;
        }

        private GameObject GetTarget()
        {
            GameObject target = null;

            var position = _weapon.transform.position;
            var radius = _weapon.ShootRadius;
            var mask = LayerUtils.CharacterMask;

            var size = Physics.OverlapSphereNonAlloc(position, radius, _colliders, mask);
            if ( size > 0 )
            {
                for( int i = 0; i < size; i++)
                {
                    if( _colliders[i].gameObject != gameObject)
                    {
                        target = _colliders[i].gameObject;
                        break;
                    }
                }
            }

            return target;
        }

    }
}