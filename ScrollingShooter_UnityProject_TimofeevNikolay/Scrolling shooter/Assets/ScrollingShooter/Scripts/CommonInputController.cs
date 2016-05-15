using UnityEngine;

namespace Assets.ScrollingShooter.Scripts
{
    public class CommonInputController : MonoBehaviour
    {
        private void Awake()
        {
            Screen.SetResolution( 486, 864, false );
        }

        private void Update()
        {
            if( Input.GetKeyDown( KeyCode.P ) )
            {
                Time.timeScale = Time.timeScale > 0f
                    ? 0
                    : 1f;
            }
        }
    }
}