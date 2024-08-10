using SampleArcade.Movement;
using UnityEngine;

namespace SampleArcade.Enemy
{
    public class EnemySprintingController : MonoBehaviour, ISprintingSource
    {
        public bool IsSprinting { get; set; }
    }
}