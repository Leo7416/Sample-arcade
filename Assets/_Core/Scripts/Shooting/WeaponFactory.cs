using UnityEngine;

namespace SampleArcade.Shooting
{
    [CreateAssetMenu(fileName = nameof(WeaponFactory), menuName = nameof(WeaponFactory))]
    public class WeaponFactory: ScriptableObject
    {
        [SerializeField]
        private WeaponView _weaponPrefab;

        [SerializeField]
        WeaponDescription _weaponDescription;

        public WeaponView Create(Transform weapontParant)
        {
            var weapon = Instantiate(_weaponPrefab, weapontParant);

            var model = new WeaponModel(_weaponDescription);
            weapon.Initialize(model);

            return weapon;
        }
    }
}