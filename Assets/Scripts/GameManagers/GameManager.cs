using SampleArcade.Enemy;
using SampleArcade.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SampleArcade.GameManagers
{
    public class GameManager : MonoBehaviour
    {
        public event Action Win;
        public event Action Loss;

        public PlayerCharacter Player { get; private set; }
        public List<EnemyCharacter> Enemies { get; private set; } = new List<EnemyCharacter>();
        public TimerUI Timer { get; private set; }

        private bool _playerRegistered = false;

        protected void Start()
        {
            Player = FindObjectOfType<PlayerCharacter>();
            if (Player != null)
            {
                RegisterPlayer(Player);
            }

            var foundEnemies = FindObjectsOfType<EnemyCharacter>();
            foreach (var enemy in foundEnemies)
            {
                RegisterEnemy(enemy);
            }

            Timer = FindObjectOfType<TimerUI>();
            if (Timer != null)
            {
                Timer.TimeEnd += PlayerLose;
            }

            Time.timeScale = 1f;
        }

        public void RegisterPlayer(PlayerCharacter player)
        {
            if (player == null) return;

            Player = player;
            Player.Dead += OnPlayerDead;
            _playerRegistered = true;
        }

        public void RegisterEnemy(EnemyCharacter enemy)
        {
            if (enemy == null) return;

            Enemies.Add(enemy);
            enemy.Dead += OnEnemyDead;
        }

        private void OnPlayerDead(BaseCharacter sender)
        {
            if (_playerRegistered)
            {
                Player.Dead -= OnPlayerDead;
                Loss?.Invoke();
                Time.timeScale = 0f;
            }
        }

        private void OnEnemyDead(BaseCharacter sender)
        {
            var enemy = sender as EnemyCharacter;
            if (enemy == null) return;

            Enemies.Remove(enemy);
            enemy.Dead -= OnEnemyDead;

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
    }
}
