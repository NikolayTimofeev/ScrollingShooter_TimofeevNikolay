using System.Collections.Generic;
using UnityEngine;

namespace Assets.ScrollingShooter.Scripts.PowerUps
{
    public class PowerUpsSpawner : MonoBehaviour
    {
        public List<PowerUpController> AvailablePowerUps = new List<PowerUpController>();
        [ Range( 0.0f, 1.0f ) ]
        public float SpawnChance = 1f;

        public static PowerUpsSpawner Instance { private set; get; }

        private void InitializeSingleton()
        {
            if( Instance != null )
            {
                Debug.LogError( "There is another instance of PowerUpsSpawner on Awake. Must not happen." );
                Destroy( Instance.gameObject );
            }

            Instance = this;
        }

        private void Awake()
        {
            InitializeSingleton();
        }

        public void SpawnWithRandomChance( Vector3 position )
        {
            if( Random.value > SpawnChance )
                return;
            var powerUpController = (PowerUpController)Instantiate( PickRandomPowerUp(), position, Quaternion.Euler( 0, 0, 0 ) );
            powerUpController.transform.parent = transform;
        }

        private PowerUpController PickRandomPowerUp()
        {
            var randomIndex = Random.Range( 0, AvailablePowerUps.Count );
            return AvailablePowerUps[randomIndex];
        }
    }
}