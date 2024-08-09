namespace SampleArcade.UI
{
    public class EnemyCounterUIModel
    {
        public int EnemyCount { get; private set; }

        public void SetEnemyCount(int count)
        {
            EnemyCount = count;
        }

        public void IncreaseEnemyCount()
        {
            EnemyCount++;
        }

        public void DecreaseEnemyCount()
        {
            if (EnemyCount > 0)
            {
                EnemyCount--;
            }
        }
    }
}