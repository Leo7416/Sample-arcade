using TMPro;
using UnityEngine;

namespace SampleArcade.UI
{
    public class EnemyCounterUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _enemyCounterText;

        public void UpdateEnemyCounter(int enemyCount)
        {
            if (_enemyCounterText != null)
            {
                _enemyCounterText.text = $"Врагов осталось: {enemyCount}";
            }
        }
    }
}