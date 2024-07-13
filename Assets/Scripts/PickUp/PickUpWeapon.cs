using SampleArcade;
using SampleArcade.Shooting;
using UnityEngine;

namespace SammpleArcade.PickUp
{
    public class PickUpWeapon : PickUpItem
    {
        [SerializeField]
        private Weapon _weaponPrefab;
        
        public override void PickUp(BaseCharacter character)
        {
            base.PickUp(character);
            character.SetWeapon(_weaponPrefab);
        }
    }
}