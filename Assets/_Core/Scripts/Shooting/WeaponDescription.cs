using System;
using UnityEngine;

namespace SampleArcade.Shooting
{
    [Serializable]
    public class WeaponDescription
    {
        [field: SerializeField]
        public float ShootRadius { get; private set; } = 5f;

        [field: SerializeField]
        public float ShootFrequencySec { get; private set; } = 1f;

        [field: SerializeField]
        public float Damage { get; private set; } = 1f;

        [field: SerializeField]
        public float BulletMaxFlyDistance { get; private set; } = 10f;

        [field: SerializeField]
        public float BulletFlySpeed { get; private set; } = 10f;
    }
}