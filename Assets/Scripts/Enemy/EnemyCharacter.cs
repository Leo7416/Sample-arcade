using SampleArcade.Movement;
using UnityEngine;

namespace SampleArcade.Enemy
{
    [RequireComponent(typeof(EnemyDirectionController), typeof(EnemyAIController), typeof(EnemySprintingController))]

    public class EnemyCharacter : BaseCharacter
    {
        [field: SerializeField]
        public float EscapeHealthPercent { get; private set; } = 30f;
        [field: SerializeField]
        public float EscapePercent { get; private set; } = 70f;
        [field: SerializeField]
        public float EscapeDistance { get; private set; } = 10f;

        public bool DecidesToRun() => Random.Range(0, 100) <= EscapePercent;
    }
}

