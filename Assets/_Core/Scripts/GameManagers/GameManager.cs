using SampleArcade.Enemy;
using SampleArcade.Player;
using SampleArcade.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SampleArcade.GameManagers
{
    public class GameManager : MonoBehaviour
    {
        public event Action Win;
        public event Action Loss;
#if UNITY_ANDROID
        [field: SerializeField]
        public Joystick Joystick { get; private set; }

        [field: SerializeField]
        public Button SprintButton { get; private set; }
#endif
        [field: SerializeField]
        public TimerUIView TimerView { get; private set; }

        [field: SerializeField]
        public EnemyCounterUIView EnemyCounterView { get; private set; }

        public PlayerCharacterView Player { get; private set; }
        public List<EnemyCharacterView> Enemies { get; private set; } = new List<EnemyCharacterView>();

        private bool _playerRegistered = false;

        public static GameManager Instance;

        protected void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        protected void Start()
        {
            TimerView.TimeEnd += PlayerLose;

            if (EnemyCounterView != null)
            {
                EnemyCounterView.SetEnemyCount(Enemies.Count);
            }

            Time.timeScale = 1f;
        }

        public void RegisterPlayer(PlayerCharacterView player)
        {
            if (player == null) return;

            Player = player;
            Player.Dead += OnPlayerDead;
            _playerRegistered = true;
        }

        public void RegisterEnemy(EnemyCharacterView enemy)
        {
            if (enemy == null) return;

            Enemies.Add(enemy);
            enemy.Dead += OnEnemyDead;

            EnemyCounterView?.RegisterEnemy();
        }

        private void OnPlayerDead(BaseCharacterView sender)
        {
            if (_playerRegistered)
            {
                Player.Dead -= OnPlayerDead;
                Loss?.Invoke();
                Time.timeScale = 0f;
            }
        }

        private void OnEnemyDead(BaseCharacterView sender)
        {
            var enemy = sender as EnemyCharacterView;
            if (enemy == null) return;

            Enemies.Remove(enemy);
            enemy.Dead -= OnEnemyDead;

            EnemyCounterView?.EnemyKilled();

            if (Enemies.Count == 0)
            {
                Win?.Invoke();
                Time.timeScale = 0f;
            }
        }

        private void PlayerLose()
        {
            TimerView.TimeEnd -= PlayerLose;
            Loss?.Invoke();
            Time.timeScale = 0f;
        }
    }
}