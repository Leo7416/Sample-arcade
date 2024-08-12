using UnityEngine;

namespace SampleArcade
{
    [CreateAssetMenu(fileName = nameof(CharacterConfig), menuName = nameof(CharacterConfig))]
    public class CharacterConfig: ScriptableObject, ICharacterConfig
    {
        [field: SerializeField]
        public float Health { get; private set; }

        [field: SerializeField]
        public float Speed { get; private set; }

        [field: SerializeField]
        [Tooltip("Aka rotation speed")]
        public float MaxRadiansDelta { get; private set; }
        [field: SerializeField]
        public float Sprint {  get; private set; }
    }
}