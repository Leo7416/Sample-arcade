using SampleArcade.PickUp;
using SampleArcade.Boosts;
using SampleArcade.Movement;
using SampleArcade.Shooting;
using UnityEngine;
using System;

namespace SampleArcade
{
    [RequireComponent(typeof(CharacterMovementController), typeof(ShootingController))]

    public abstract class BaseCharacter : MonoBehaviour
    {
        public event Action<BaseCharacter> Dead;

        [SerializeField]
        private Animator _animator;

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
        private SpeedBoost _currentBoost;
        private Weapon _currentWeapon;

        private float _currentHealth;
        private float _deadAnimationTimeSeconds = 4.5f;

        protected void Awake()
        {
            _movementDirectionSource = GetComponent<IMovementDirectionSource>();
            _sprintingSource = GetComponent<ISprintingSource>();

            _characterMovementController = GetComponent<CharacterMovementController>();
            _shootingController = GetComponent<ShootingController>();
            _currentBoost = GetComponent<SpeedBoost>();

            _currentWeapon = _baseWeaponPrefab;
            _currentHealth = _health;
        }

        protected void Start()
        {
            SetWeapon(_currentWeapon);
        }

        protected void Update()
        {
            if (_currentHealth <= 0)
            {
                _characterMovementController.MovementDirection = Vector3.zero;
                _characterMovementController.LookDirection = Vector3.zero;
                _characterMovementController.SetAlive(false);
                _shootingController.SetAlive(false);

                _animator.SetTrigger("IsDying");
                Dead?.Invoke(this);
                Destroy(gameObject, _deadAnimationTimeSeconds);
            }

            var direction = _movementDirectionSource.MovementDirection;
            var lookDirection = direction;
            if (_shootingController.HasTarget)
                lookDirection = (_shootingController.TargetPosition - transform.position).normalized;

            _characterMovementController.MovementDirection = direction;
            _characterMovementController.LookDirection = lookDirection;

            _characterMovementController.SetSprint(_sprintingSource.IsSprinting);

            bool isRunning = _sprintingSource.IsSprinting || (_currentBoost != null && _currentBoost.HasBoost);

            _animator.SetBool("IsWalking", direction != Vector3.zero);
            _animator.SetBool("IsShooting", _shootingController.HasTarget);
            _animator.SetBool("IsRunning", isRunning);
            _animator.SetBool("IsBackwards",
                Mathf.Abs(Mathf.Sign(direction.x) - Mathf.Sign(lookDirection.x)) > Mathf.Epsilon ||
                Mathf.Abs(Mathf.Sign(direction.z) - Mathf.Sign(lookDirection.z)) > Mathf.Epsilon);
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
            _currentBoost = boost;
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

