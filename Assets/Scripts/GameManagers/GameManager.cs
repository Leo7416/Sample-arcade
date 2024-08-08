using SampleArcade.Enemy;
using SampleArcade.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SampleArcade.GameManagers
{
    public class GameManager : MonoBehaviour
    {
        public event Action Win;
        public event Action Loss;

        public PlayerCharacterView Player { get; private set; }
        public List<EnemyCharacterView> Enemies { get; private set; } = new List<EnemyCharacterView>();
        public TimerUI Timer { get; private set; }
        public EnemyCounterUI EnemyCounterUI { get; private set; }

        private bool _playerRegistered = false;
        private int _enemyCount;

        protected void Start()
        {
            Player = FindObjectOfType<PlayerCharacterView>();
            if (Player != null)
            {
                RegisterPlayer(Player);
            }

            var foundEnemies = FindObjectsOfType<EnemyCharacterView>();
            foreach (var enemy in foundEnemies)
            {
                RegisterEnemy(enemy);
            }

            Timer = FindObjectOfType<TimerUI>();
            if (Timer != null)
            {
                Timer.TimeEnd += PlayerLose;
            }

            EnemyCounterUI = FindObjectOfType<EnemyCounterUI>();
            UpdateEnemyCounterUI();

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
            _enemyCount++;
            UpdateEnemyCounterUI();
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
            _enemyCount--;
            UpdateEnemyCounterUI();

            if (Enemies.Count == 0)
            {
                Win?.Invoke();
                Time.timeScale = 0f;
            }
        }

        private void PlayerLose()
        {
            if (Timer != null)
            {
                Timer.TimeEnd -= PlayerLose;
            }
            Loss?.Invoke();
            Time.timeScale = 0f;
        }

        private void UpdateEnemyCounterUI()
        {
            if (EnemyCounterUI != null)
            {
                EnemyCounterUI.UpdateEnemyCounter(_enemyCount);
            }
        }
    }
}