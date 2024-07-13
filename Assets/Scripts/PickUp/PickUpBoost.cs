using SampleArcade;
using SampleArcade.Boosts;
using UnityEngine;

namespace SammpleArcade.PickUp
{
    public class PickUpBoost : PickUpItem
    {
        [SerializeField]
        private Boost _boostPrefab;
        
        public override void PickUp(BaseCharacter character)
        {
            base.PickUp(character);
            character.ActivateBoost(_boostPrefab);
        }
    }
}