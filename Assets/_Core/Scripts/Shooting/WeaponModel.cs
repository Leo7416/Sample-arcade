using System;
using UnityEngine;

namespace SampleArcade.Shooting
{
    public class WeaponModel
    {
        public event Action<Vector3, WeaponDescription> Shot;

        public WeaponDescription Description {  get; }

        public float DamageMultiplier { get; private set; } = 1f;

        public WeaponModel(WeaponDescription description)
        {
            Description = description;
        }

        public void Shoot(Vector3 shotPosition, Vector3 targetPoint)
        {
            var target = targetPoint - shotPosition;
            target.y = 0;
            target.Normalize();

            Shot?.Invoke(target, Description);
        }

         public void SetDamageMultiplier(float multiplier)
        {
            DamageMultiplier = multiplier;
        }
    }
}