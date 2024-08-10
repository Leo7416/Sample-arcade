using SampleArcade.Shooting;
using UnityEngine;

namespace SampleArcade.PickUp
{
    public class PickUpWeapon : PickUpItem
    {
        [SerializeField]
        private WeaponFactory _weaponFactory;
        
        public override void PickUp(BaseCharacterView character)
        {
            base.PickUp(character);
            character.SetWeapon(_weaponFactory);
        }
    }
}