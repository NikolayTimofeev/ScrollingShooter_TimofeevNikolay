using UnityEngine;

namespace Assets.ScrollingShooter.Scripts.PowerUps
{
    public class PowerUpController : MonoBehaviour
    {
        public float Speed;
        public PowerUpType PowerUp;

        private void Start()
        {
            SetSpeed( Speed );
        }

        public void SetSpeed( float speed )
        {
            rigidbody2D.velocity = -transform.up*speed;
        }

        private void OnTriggerEnter2D( Collider2D otherCollider )
        {
            if( otherCollider.CompareTag( "Bullet" ) )
                return;

            var powerUpable = otherCollider.GetComponent( typeof( IPowerUpable ) ) as IPowerUpable;
            if( powerUpable == null )
                return;

            powerUpable.UsePowerUp( PowerUp );

            Destroy( gameObject );
        }
    }
}