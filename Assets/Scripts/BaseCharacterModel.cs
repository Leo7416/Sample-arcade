using SampleArcade.Movement;
using SampleArcade.Shooting;
using UnityEngine;
using System;

namespace SampleArcade
{
    public class BaseCharacterModel
    {
        public event Action Dead;

        public bool IsShooting => _shootingController.HasTarget;
        public bool IsDead { get; private set; } = false;

        public TransformModel Transform { get; private set; }
  
        public float Health { get; private set; }
        public float CurrentHealth { get; private set; }

        private readonly IMovementController _characterMovementController;
        private readonly ShootingController _shootingController;

        public BaseCharacterModel(IMovementController movementController, 
            ShootingController shootingController, ICharacterConfig config)
        {
            _characterMovementController = movementController;
            _shootingController = shootingController;

            Health = config.Health;
            CurrentHealth = Health;
        }

        public void Initialize(Vector3 position, Quaternion rotation)
        {
            Transform = new TransformModel(position, rotation);
        }

        public void Move(Vector3 direction, bool IsRunnig)
        {
            var lookDirection = direction;
            if (_shootingController.HasTarget)
                lookDirection = (_shootingController.TargetPosition - Transform.Position).normalized;

            Transform.Position += _characterMovementController.Translate(direction);
            Transform.Rotation = _characterMovementController.Rotate(Transform.Rotation, lookDirection);
            _characterMovementController.SetSprint(IsRunnig);
            
        }

        public void Damage(float damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
                IsDead = true;
                Dead?.Invoke();
            }
        }

        public void TryShoot(Vector3 shotPosition)
        {
            if (!IsDead)
                _shootingController.TryShoot(shotPosition);
        }

        public void SetWeapon(WeaponModel weapon)
        {   
            _shootingController.SetWeapon(weapon);
        }

        public void MultiplySpeedBoost(float boostSpeed)
        {
            _characterMovementController.MultiplySpeedBoost(boostSpeed);
        }
    }
}