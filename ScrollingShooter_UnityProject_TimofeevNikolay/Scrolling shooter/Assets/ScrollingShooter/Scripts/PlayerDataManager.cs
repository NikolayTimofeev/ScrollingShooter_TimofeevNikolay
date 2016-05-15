using System;
using UnityEngine;

namespace Assets.ScrollingShooter.Scripts
{
    public class PlayerDataManager
    {
        private static readonly PlayerDataManager SingletonInstance = new PlayerDataManager();

        private PlayerDataManager()
        {
            HighScore = PlayerPrefs.GetInt( "HighScore" );
        }

        public static PlayerDataManager Instance
        {
            get { return SingletonInstance; }
        }

        private int _currentScore;

        public int CurrentScore
        {
            get { return _currentScore; }
            private set
            {
                _currentScore = value;
                if( ScoreChanged != null )
                    ScoreChanged( _currentScore );
            }
        }

        public event Action<int> ScoreChanged;

        private const int MaxHealth = 3;
        public event Action<int> HealthValueChanged;

        private int _health = MaxHealth;

        public int Health
        {
            get { return _health; }
            private set
            {
                _health = value;
                if( HealthValueChanged != null )
                    HealthValueChanged( _health );
            }
        }

        public int HighScore { get; private set; }

        public void AddScore( int score )
        {
            CurrentScore += score;
        }

        public void TakeDamage( int damage )
        {
            Health -= damage;

            if( Health <= 0 )
            {
                if( CurrentScore > HighScore )
                {
                    HighScore = CurrentScore;
                    PlayerPrefs.SetInt( "HighScore", CurrentScore );
                }
                EnemySpawner.Instance.StopSpawning();
                PlayerShipController.Instance.Die();
                GuiHelper.Instance.ShowGameOverScreen();
            }
        }

        public void Heal( int healValue )
        {
            if( Health == MaxHealth )
                return;

            Health = Mathf.Min( Health + healValue, MaxHealth );
        }

        public void Reset()
        {
            CurrentScore = 0;
            Health = MaxHealth;
        }
    }
}