using UnityEngine;

namespace Assets.ScrollingShooter.Scripts
{
    [ RequireComponent( typeof( Collider2D ) ) ]
    public class GameFieldBoundaryController : MonoBehaviour
    {
        public static GameFieldBoundaryController Instance { private set; get; }

        private void Awake()
        {
            InitializeSingleton();

            Xmin = transform.position.x - transform.localScale.x/2;
            Xmax = transform.position.x + transform.localScale.x/2;
            Ymin = transform.position.y - transform.localScale.y/2;
            Ymax = transform.position.y + transform.localScale.y/2;
        }

        public float Xmin { private set; get; }
        public float Xmax { private set; get; }
        public float Ymin { private set; get; }
        public float Ymax { private set; get; }

        private void OnTriggerExit2D( Collider2D otherCollider )
        {
            Destroy( otherCollider.gameObject );
        }

        private void InitializeSingleton()
        {
            if( Instance != null )
            {
                Debug.LogError( "There is another instance of GameFieldBoundaryController on Awake. Must not happen." );
                Destroy( Instance.gameObject );
            }

            Instance = this;
        }
    }
}