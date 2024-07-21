using SampleArcade.PickUp;
using SampleArcade.Boosts;
using SampleArcade.Movement;
using SampleArcade.Shooting;
using UnityEngine;
using SampleArcade.Enemy;
using System.Linq;

namespace SampleArcade
{
    [RequireComponent(typeof(CharacterMovementController), typeof(ShootingController))]

    public abstract class BaseCharacter : MonoBehaviour
    {
        [SerializeField]
        private Weapon _baseWeaponPrefab;

        [SerializeField]
        private Transform _hand;

        [SerializeField]
        private float _health = 10f;

        [SerializeField]
        private float _lowHealthThreshold = 3f;

        private IMovementDirectionSource _movementDirectionSource;
        private CharacterMovementController _characterMovementController;
        private ShootingController _shootingController;

        public float Health => _health;
        public float LowHealthThreshold => _lowHealthThreshold;
        public bool HasPickedUpWeapon => _hasPickedUpWeapon;

        private bool _hasPickedUpWeapon = false;
       
        protected void Awake()
        {
            _movementDirectionSource = GetComponent<IMovementDirectionSource>();

            _characterMovementController = GetComponent<CharacterMovementController>();
            _shootingController = GetComponent<ShootingController>();
        }

        protected void Start()
        {
            SetWeapon(_baseWeaponPrefab);
        }

        protected void Update()
        {
            var direction = _movementDirectionSource.MovementDirection;
            var lookDirection = direction;
            if (_shootingController.HasTarget)
                lookDirection = (_shootingController.TargetPosition - transform.position).normalized;

            _characterMovementController.MovementDirection = direction;
            _characterMovementController.LookDirection = lookDirection;

            if (_health <= 0)
                Destroy(gameObject);
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (LayerUtils.IsBullet(other.gameObject))
            {
                var bullet = other.gameObject.GetComponent<Bullet>();

                _health -= bullet.Damage;

                Destroy(other.gameObject);
            }
            else 
            {
                var pickUpItem = other.gameObject.GetComponent<PickUpItem>();
                if (pickUpItem != null)
                {
                    pickUpItem.PickUp(this);
                    _hasPickedUpWeapon = true;
                    Destroy(other.gameObject);
                }
            }
            
        }

        public void SetWeapon(Weapon weapon)
        {
            _shootingController.SetWeapon(weapon, _hand);
        }

        public void ActivateBoost(Boost boost)
        {
            _characterMovementController.ApplyBoost(boost);
        }
    }
}

