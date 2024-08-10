using TMPro;
using UnityEngine;

namespace SampleArcade.UI
{
    public class EnemyCounterUIView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _enemyCounterText;

        private EnemyCounterUIModel Model;

        private void Start()
        {
            Model = new EnemyCounterUIModel();
        }

        public void RegisterEnemy()
        {
            if (Model != null)
            {
                Model.IncreaseEnemyCount();
                UpdateEnemyCounter();
            }
        }

        public void EnemyKilled()
        {
            if (Model != null)
            {
                Model.DecreaseEnemyCount();
                UpdateEnemyCounter();
            }
        }

        public void SetEnemyCount(int count)
        {
            if (Model != null)
            {
                Model.SetEnemyCount(count);
                UpdateEnemyCounter();
            }
        }

        private void UpdateEnemyCounter()
        {
            if (_enemyCounterText != null && Model != null)
            {
                _enemyCounterText.text = $"Врагов осталось: {Model.EnemyCount}";
            }
        }
    }
}