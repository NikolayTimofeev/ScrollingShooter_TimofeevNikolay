using Assets.ScrollingShooter.Scripts.Bullets;
using Assets.ScrollingShooter.Scripts.Helpers;
using UnityEngine;

namespace Assets.ScrollingShooter.Scripts.Weapons
{
    public class OneShotGun : MonoBehaviour, IWeapon
    {
        public float SecondsBetweenShots;
        public int Damage;
        public float BulletSpeed;
        public BulletController BulletPrefb;
        public bool IsAutoFire = true;

        private float _cooldown;

        private void Start() {}

        private void FixedUpdate()
        {
            if( _cooldown > 0f )
                _cooldown -= Time.deltaTime;

            if( IsAutoFire )
                Shoot();
        }

        public void Shoot()
        {
            if( _cooldown > 0f )
                return;

            _cooldown = SecondsBetweenShots;
            var bullet = (BulletController)Instantiate( BulletPrefb, transform.position, transform.rotation );
            UnityHelper.SetLayerRecursively( bullet.gameObject, gameObject.layer );
            bullet.SetSpeed( BulletSpeed );
            bullet.SetDamage( Damage );
        }

        public void AutoFireStart()
        {
            IsAutoFire = true;
        }

        public void AutoFireStop()
        {
            IsAutoFire = false;
        }
    }
}