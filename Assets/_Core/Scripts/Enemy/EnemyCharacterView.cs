using UnityEngine;

namespace SampleArcade.Enemy
{
    [RequireComponent(typeof(EnemyDirectionController), typeof(EnemyAIControllerView), typeof(EnemySprintingController))]

    public class EnemyCharacterView : BaseCharacterView
    {
        [field: SerializeField]
        public float EscapeHealthPercent { get; private set; } = 30f;
        [field: SerializeField]
        public float EscapePercent { get; private set; } = 70f;
        [field: SerializeField]
        public float EscapeDistance { get; private set; } = 10f;

        public bool DecidesToRun() => UnityEngine.Random.Range(0, 100) <= EscapePercent;
    }
}

