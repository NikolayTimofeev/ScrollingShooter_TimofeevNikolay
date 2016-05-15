using UnityEngine;
using UnityEngine.UI;

namespace Assets.ScrollingShooter.Scripts.UI
{
    [ RequireComponent( typeof( Text ) ) ]
    public class ScoreValueHandler : MonoBehaviour
    {
        private Text _textComponent;

        private void Awake()
        {
            _textComponent = GetComponent<Text>();
        }

        private void ChangeScore( int score )
        {
            _textComponent.text = score.ToString();
        }

        private void OnEnable()
        {
            PlayerDataManager.Instance.ScoreChanged += ChangeScore;
        }

        private void OnDisable()
        {
            PlayerDataManager.Instance.ScoreChanged -= ChangeScore;
        }
    }
}