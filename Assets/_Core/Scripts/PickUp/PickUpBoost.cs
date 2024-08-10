using SampleArcade.Boosts;
using UnityEngine;

namespace SampleArcade.PickUp
{
    public class PickUpBoost : PickUpItem
    {
        [SerializeField]
        private Boost _boostPrefab;

        public override void PickUp(BaseCharacterView character)
        {
            base.PickUp(character);
            character.ActivateBoost(_boostPrefab);
        }
    }
}