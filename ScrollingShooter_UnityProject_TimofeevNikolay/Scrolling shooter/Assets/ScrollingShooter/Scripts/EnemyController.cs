using Assets.ScrollingShooter.Scripts.PowerUps;
using UnityEngine;

namespace Assets.ScrollingShooter.Scripts
{
    [ RequireComponent( typeof( SpriteRenderer ) ) ]
    [ RequireComponent( typeof( Collider2D ) ) ]
    [ RequireComponent( typeof( Rigidbody2D ) ) ]
    public class EnemyController : MonoBehaviour, IDamageable
    {
        public float Speed;
        public int Health;
        public int ScoreReward;
        public int CollisionDamage;

        private void Start()
        {
            SetSpeed( Speed );
        }

        public void SetSpeed( float speed )
        {
            rigidbody2D.velocity = transform.up*speed;
        }

        public void TakeDamage( int damage )
        {
            Health -= damage;
            if( Health <= 0 )
            {
                PlayerDataManager.Instance.AddScore( ScoreReward );
                PowerUpsSpawner.Instance.SpawnWithRandomChance( transform.position );
                Destroy( gameObject );
            }
        }

        private void OnTriggerEnter2D( Collider2D otherCollider )
        {
            if( otherCollider.CompareTag( "Bullet" ) )
                return;

            var damagable = otherCollider.GetComponent( typeof( IDamageable ) ) as IDamageable;
            if( damagable == null )
                return;

            damagable.TakeDamage( CollisionDamage );

            Destroy( gameObject );
        }
    }
}