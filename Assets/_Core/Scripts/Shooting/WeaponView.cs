using UnityEngine;

namespace SampleArcade.Shooting
{
    public class WeaponView : MonoBehaviour
    {
        public WeaponModel Model { get; private set; }

        [field: SerializeField]
        public Transform BulletSpawnPosition {  get; private set; }

        [SerializeField]
        private BulletView _bulletPrefab;

        [SerializeField]
        private ParticleSystem _shootParticle;

        [SerializeField]
        private AudioSource _shootSound;

        public void Initialize(WeaponModel model)
        {
            if (Model != null)
            {
                Debug.LogWarning("Weapon model has been already initialized!");
                return;
            }

            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            Model = model;
            Model.Shot += Shoot;
        }

        protected void OnDestroy()
        {
            if (Model != null)
                Model.Shot -= Shoot;
        }

        public void Shoot(Vector3 targetDirection, WeaponDescription description)
        {
            var bullet = Instantiate(_bulletPrefab, BulletSpawnPosition.position, Quaternion.identity);
            bullet.Initialize(description.Damage * Model.DamageMultiplier, targetDirection, 
                description.BulletMaxFlyDistance, description.BulletFlySpeed);

            _shootParticle.Play();
            _shootSound.Play();
        }
    }
}