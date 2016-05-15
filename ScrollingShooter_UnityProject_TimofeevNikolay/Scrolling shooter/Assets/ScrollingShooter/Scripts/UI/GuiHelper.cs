using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.ScrollingShooter.Scripts
{
    public class GuiHelper : MonoBehaviour
    {
        public GameObject GameOverCanvas;
        public Text HighScoreLabel;

        public static GuiHelper Instance { private set; get; }

        private void InitializeSingleton()
        {
            if( Instance != null )
            {
                Debug.LogError( "There is another instance of GuiHelper on Awake. Must not happen." );
                Destroy( Instance.gameObject );
            }

            Instance = this;
        }

        private void Awake()
        {
            InitializeSingleton();
        }

        public void ShowGameOverScreen()
        {
            HighScoreLabel.text = String.Format( HighScoreLabel.text, PlayerDataManager.Instance.CurrentScore,
                                                 PlayerDataManager.Instance.HighScore );
            GameOverCanvas.SetActive( true );
            _isGameOver = true;
        }

        private bool _isGameOver;

        private void Update()
        {
            if( !_isGameOver )
                return;

            if( Input.GetKeyDown( KeyCode.Space ) ||
                ( Application.isMobilePlatform && Input.touchCount > 0 ) )
            {
                PlayerDataManager.Instance.Reset();
                Application.LoadLevel( Application.loadedLevel );
                _isGameOver = false;
            }
        }
    }
}