using System.Collections.Generic;
using UnityEngine;

namespace Assets.ScrollingShooter.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        public List<EnemyController> EnemyPrefabs = new List<EnemyController>();
        public float SecondsBetweenEnemySpawns;

        private float _cooldown;
        private bool _canSpawnEnemies = true;

        public static EnemySpawner Instance { private set; get; }

        private void InitializeSingleton()
        {
            if( Instance != null )
            {
                Debug.LogError( "There is another instance of EnemySpawner on Awake. Must not happen." );
                Destroy( Instance.gameObject );
            }

            Instance = this;
        }

        private void Awake()
        {
            InitializeSingleton();
        }

        private void FixedUpdate()
        {
            if( _cooldown > 0f )
                _cooldown -= Time.deltaTime;

            Spawn();
        }

        private void Spawn()
        {
            if( !_canSpawnEnemies ||
                _cooldown > 0f )
                return;

            var yCoordianteToSpawn = GameFieldBoundaryController.Instance.Ymax +
                                     ( GameFieldBoundaryController.Instance.Ymax - GameFieldBoundaryController.Instance.Ymin )/10;

            var xCoordinateToSpawn = Random.Range( GameFieldBoundaryController.Instance.Xmin,
                                                   GameFieldBoundaryController.Instance.Xmax );
            var rotationOverZ = Random.Range( -15f, 15f );
            var enemyGameObject =
                (EnemyController)
                    Instantiate( PickRandomEnemy(), new Vector2( xCoordinateToSpawn, yCoordianteToSpawn ),
                                 Quaternion.Euler( 0, 0, 180 + rotationOverZ ) );
            enemyGameObject.transform.parent = transform;
            _cooldown = SecondsBetweenEnemySpawns;
        }

        private EnemyController PickRandomEnemy()
        {
            var randomIndex = Random.Range( 0, EnemyPrefabs.Count );
            return EnemyPrefabs[randomIndex];
        }

        public void StopSpawning()
        {
            _canSpawnEnemies = false;
            //DestroyAllEnemies();
        }

        public void StartSpawning()
        {
            _canSpawnEnemies = true;
        }

        public void DestroyAllEnemies()
        {
            foreach( Transform child in transform )
                Destroy( child.gameObject );
        }
    }
}