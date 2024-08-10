using System;

namespace SampleArcade.UI
{
    public class TimerUIModel
    {
        public event Action TimeEnd;

        public float GameDurationSeconds { get; private set; }
        public float TimerSeconds { get; private set; }
        private bool _timerEnd;

        public void Initialize(float gameDurationSeconds)
        {
            GameDurationSeconds = gameDurationSeconds;
            TimerSeconds = 0;
            _timerEnd = false;
        }

        public void UpdateTime(float deltaTime)
        {
            if (_timerEnd) return;

            TimerSeconds += deltaTime;

            if (TimerSeconds >= GameDurationSeconds)
            {
                TimeEnd?.Invoke();
                _timerEnd = true;
            }
        }

        public int GetRemainingTime()
        {
            return (int)(GameDurationSeconds - TimerSeconds);
        }

        public bool IsTimeEnded()
        {
            return _timerEnd;
        }
    }
}