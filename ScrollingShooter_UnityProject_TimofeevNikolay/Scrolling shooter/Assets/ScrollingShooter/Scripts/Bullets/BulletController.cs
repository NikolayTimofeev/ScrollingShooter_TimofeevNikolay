using UnityEngine;

namespace Assets.ScrollingShooter.Scripts.Bullets
{
    [ RequireComponent( typeof( Collider2D ) ) ]
    [ RequireComponent( typeof( Rigidbody2D ) ) ]
    public class BulletController : MonoBehaviour
    {
        private int _damage;
        public bool IsDestroyedOnHit = true;

        public void SetSpeed( float speed )
        {
            rigidbody2D.velocity = transform.up*speed;
        }

        public void SetDamage( int damage )
        {
            _damage = damage;
        }

        private void OnTriggerEnter2D( Collider2D otherCollider )
        {
            if( otherCollider.CompareTag( "Bullet" ) )
                return;

            var damagable = otherCollider.GetComponent( typeof( IDamageable ) ) as IDamageable;
            if( damagable == null )
                return;

            damagable.TakeDamage( _damage );

            if( IsDestroyedOnHit )
                Destroy( gameObject );
        }
    }
}