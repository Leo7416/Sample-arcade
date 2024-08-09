using SampleArcade.Timer;
using System;
using TMPro;
using UnityEngine;

namespace SampleArcade.UI
{
    public class TimerUIView : MonoBehaviour
    {
        public event Action TimeEnd;

        [SerializeField]
        private TextMeshProUGUI _outputText;

        [SerializeField]
        private float gameDurationSeconds = 120f;

        private ITimer _timer;
        private string _format;

        private TimerUIModel Model;

        private void Awake()
        {
            _format = _outputText.text;
            _timer = new UnityTimer();

            Model = new TimerUIModel();
            Model.Initialize(gameDurationSeconds);
        }

        private void Update()
        {
            Model.UpdateTime(_timer.DeltaTime);

            int time = Model.GetRemainingTime();
            _outputText.text = string.Format(_format, time / 60, time % 60);

            if (Model.IsTimeEnded())
            {
                TimeEnd?.Invoke();
            }
        }
    }
}