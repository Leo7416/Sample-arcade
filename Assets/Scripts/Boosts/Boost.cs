using UnityEngine;

namespace SampleArcade.Boosts
{
    public class Boost : MonoBehaviour
    {
        [field: SerializeField]
        public float SprintMultiplier { get; private set; } = 2f;
        [field: SerializeField]
        public float Duration { get; private set; } = 5f;

        public void Initialize(float sprintMultiplier, float duration)
        {
            SprintMultiplier = sprintMultiplier;
            Duration = duration;
        }

    }
}
