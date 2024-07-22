using SampleArcade.PickUp;
using SampleArcade.Boosts;
using SampleArcade.Movement;
using SampleArcade.Shooting;
using UnityEngine;

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

        private IMovementDirectionSource _movementDirectionSource;
        private ISprintingSource _sprintingSource;
        private CharacterMovementController _characterMovementController;
        private ShootingController _shootingController;

        private float _currentHealth;
        private Weapon _currentWeapon;

       
        protected void Awake()
        {
            _movementDirectionSource = GetComponent<IMovementDirectionSource>();
            _sprintingSource = GetComponent<ISprintingSource>();

            _characterMovementController = GetComponent<CharacterMovementController>();
            _shootingController = GetComponent<ShootingController>();

            _currentWeapon = _baseWeaponPrefab;
            _currentHealth = _health;
        }

        protected void Start()
        {
            SetWeapon(_currentWeapon);
        }

        protected void Update()
        {
            var direction = _movementDirectionSource.MovementDirection;
            var lookDirection = direction;
            if (_shootingController.HasTarget)
                lookDirection = (_shootingController.TargetPosition - transform.position).normalized;

            _characterMovementController.MovementDirection = direction;
            _characterMovementController.LookDirection = lookDirection;

            _characterMovementController.SetSprint(_sprintingSource.IsSprinting);

            if (_currentHealth <= 0)
                Destroy(gameObject);
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (LayerUtils.IsBullet(other.gameObject))
            {
                var bullet = other.gameObject.GetComponent<Bullet>();

                _currentHealth -= bullet.Damage;

                Destroy(other.gameObject);
            }
            else 
            {
                var pickUpItem = other.gameObject.GetComponent<PickUpItem>();
                if (pickUpItem != null)
                {
                    pickUpItem.PickUp(this);
                    Destroy(other.gameObject);
                }
            }
            
        }

        public void SetWeapon(Weapon weapon)
        {
            _shootingController.SetWeapon(weapon, _hand);
            _currentWeapon = weapon;
        }

        public void ActivateBoost(SpeedBoost boost)
        {
            boost.ActivateBoost(this);
        }

        public void MultiplySpeed(float boost)
        {
            _characterMovementController.MultiplySpeed(boost);
        }

        public void ResetSpeed()
        {
            _characterMovementController.ResetSpeed();
        }

        public float GetHealthPercent()
        {
            return _currentHealth / _health * 100f;
        }

        public bool HasBaseWeapon()
        {
            return _currentWeapon == _baseWeaponPrefab;
        }
    }
}

