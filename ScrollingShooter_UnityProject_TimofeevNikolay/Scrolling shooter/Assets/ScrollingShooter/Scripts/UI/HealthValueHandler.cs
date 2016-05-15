using UnityEngine;
using UnityEngine.UI;

namespace Assets.ScrollingShooter.Scripts.UI
{
    [ RequireComponent( typeof( Slider ) ) ]
    public class HealthValueHandler : MonoBehaviour
    {
        private Slider _sliderComponent;

        private void Awake()
        {
            _sliderComponent = GetComponent<Slider>();
        }

        private void ChangeHealthValue( int health )
        {
            _sliderComponent.value = health;
        }

        private void OnEnable()
        {
            PlayerDataManager.Instance.HealthValueChanged += ChangeHealthValue;
        }

        private void OnDisable()
        {
            PlayerDataManager.Instance.HealthValueChanged -= ChangeHealthValue;
        }
    }
}