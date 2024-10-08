using SampleArcade.PickUp;
using SampleArcade.Movement;
using SampleArcade.Shooting;
using UnityEngine;
using System;
using SampleArcade.UI;
using SampleArcade.Boosts;

namespace SampleArcade
{
    [RequireComponent(typeof(CharacterController), typeof(Animator))]

    public abstract class BaseCharacterView : MonoBehaviour
    {
        public event Action<BaseCharacterView> Dead;

        private Animator _animator;

        [SerializeField]
        private WeaponFactory _baseWeapon;

        [SerializeField]
        private Transform _hand;

        [SerializeField]
        private ParticleSystem _hitParticle;

        [SerializeField]
        private ParticleSystem _deadParticle;

        [SerializeField]
        private AudioSource _hitSound;

        [SerializeField]
        private AudioSource _deadSound;

        [SerializeField]
        private ParticleSystem _speedBoostParticle;

        [SerializeField]
        private ParticleSystem _damageBoostParticle;

        private IMovementDirectionSource _movementDirectionSource;
        private ISprintingSource _sprintingSource;
        private CharacterController _characterController;
        private HealthBarUIView _healthBarUI;
        private WeaponView _weapon;
        private WeaponFactory _currentWeapon;

        public BaseCharacterModel Model { get; private set; }
        public bool IsSpeedBoostActivate { get; private set; }
        public bool IsDamageBoostActivate { get; private set; }

        private float _deadAnimationTimeSeconds = 4.5f;
        private float _speedBoostMultiplier = 1f;
   
        protected void Awake()
        {
            _animator = GetComponent<Animator>();
            _movementDirectionSource = GetComponent<IMovementDirectionSource>();
            _sprintingSource = GetComponent<ISprintingSource>();
            _characterController = GetComponent<CharacterController>();
            _healthBarUI = GetComponentInChildren<HealthBarUIView>();
        }

        protected void Start()
        {
            SetWeapon(_baseWeapon);
        }

        public void Initialize(BaseCharacterModel model)
        {
            Model = model;
            Model.Initialize(transform.position, transform.rotation);
            Model.Dead += OnDeath;

            _currentWeapon = _baseWeapon;
            _healthBarUI.UpdateHealth(Model.CurrentHealth);
        }

        protected void Update()
        {
            if (Model.IsDead) return;

            Model.Move(_movementDirectionSource.MovementDirection, 
                _sprintingSource.IsSprinting);
            Model.TryShoot(_weapon.BulletSpawnPosition.position);

            if (IsSpeedBoostActivate)
            {
                Model.MultiplySpeedBoost(_speedBoostMultiplier);
            }
            else
            {
                _speedBoostMultiplier = 1f;
            }

            var moveDelta = Model.Transform.Position - transform.position;
            _characterController.Move(moveDelta);
            Model.Transform.Position = transform.position;

            transform.rotation = Model.Transform.Rotation;

            var lookDirection = _movementDirectionSource.MovementDirection;
            bool isBackwards = Vector3.Dot(transform.forward, lookDirection) < 0;

            _animator.SetBool("IsWalking", moveDelta != Vector3.zero);
            _animator.SetBool("IsShooting", Model.IsShooting);
            _animator.SetBool("IsRunning", _sprintingSource.IsSprinting || IsSpeedBoostActivate);
            _animator.SetBool("IsBackwards", isBackwards);
        }

        protected void OnDestroy()
        {
            if (Model != null)
                Model.Dead -= OnDeath;
        }

        protected void OnTriggerEnter(Collider other)
        {
            var pickUpItem = other.gameObject.GetComponent<PickUpItem>();
            if (pickUpItem != null)
            {
                pickUpItem.PickUp(this);
                Destroy(other.gameObject);
            }
        }

        private void OnDeath()
        { 
            _animator.SetTrigger("IsDying");
            _deadParticle.Play();
            _deadSound.Play();

            _characterController.enabled = false;

            Dead?.Invoke(this);

            Destroy(gameObject, _deadAnimationTimeSeconds);
        }

        public void OnBulletHit(BulletView bullet)
        {
            Model.Damage(bullet.Damage);
            _healthBarUI.UpdateHealth(Model.CurrentHealth);

            _hitParticle.Play();
            _hitSound.Play();
        }

        public void SetWeapon(WeaponFactory weaponFactory)
        {
            if (_weapon != null)
                Destroy(_weapon.gameObject);

            _weapon = weaponFactory.Create(_hand);
            _currentWeapon = weaponFactory;

            Model.SetWeapon(_weapon.Model);
        }

        public bool HasBaseWeapon()
        {
           return _currentWeapon == _baseWeapon;
        }

        public float GetHealthPercent()
        {
            return Model.CurrentHealth / Model.Health * 100f;
        }

        public void ActivateBoost(Boost boost)
        {
            boost.ActivateBoost(this);
        }

        public void ActivateSpeedBoostEffect(float multiplier, float duration)
        {
            _speedBoostMultiplier = multiplier;
            IsSpeedBoostActivate = true;

            _speedBoostParticle.Play();
            Invoke(nameof(DeactivateSpeedBoostEffect), duration);
        }

        public void DeactivateSpeedBoostEffect()
        {
             IsSpeedBoostActivate = false;
            _speedBoostParticle.Stop();
        }

        public void ActivateDamageBoostEffect(float multiplier, float duration)
        {
            Model.MultiplyDamageBoost(multiplier);
            IsDamageBoostActivate = true;

            _damageBoostParticle.Play();
            Invoke(nameof(DeactivateDamageBoostEffect), duration);
        }

        public void DeactivateDamageBoostEffect()
        {
            Model.MultiplyDamageBoost(1f);
            IsDamageBoostActivate = false;

            _damageBoostParticle.Stop();
        }
    }
}