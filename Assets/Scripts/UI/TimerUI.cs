using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace SampleArcade.UI
{
    public class TimerUI : MonoBehaviour
    {
        public event Action TimeEnd;

        [SerializeField]
        private TextMeshProUGUI _outputText;
        private string _format;
        private bool _timerEnd;

        [field:SerializeField]
        public float GameDurationSeconds { get; private set; }
        public float TimerSeconds { get; private set; }

        protected void Start()
        {
            _format = _outputText.text;
            _timerEnd = false;
        }

        protected void Update()
        {
            if (_timerEnd) return;

            TimerSeconds += Time.deltaTime;
            if (TimerSeconds >= GameDurationSeconds)
            {
                TimeEnd?.Invoke();
                _timerEnd = true;
            }

            int time = (int)(GameDurationSeconds - TimerSeconds);
            _outputText.text = string.Format(_format, time / 60, time % 60); 
        }
    }
}