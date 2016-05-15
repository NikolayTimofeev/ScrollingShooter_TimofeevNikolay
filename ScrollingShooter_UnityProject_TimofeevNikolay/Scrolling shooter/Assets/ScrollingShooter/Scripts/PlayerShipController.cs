using Assets.ScrollingShooter.Scripts.PowerUps;
using Assets.ScrollingShooter.Scripts.Weapons;
using UnityEngine;

namespace Assets.ScrollingShooter.Scripts
{
    [ RequireComponent( typeof( SpriteRenderer ) ) ]
    [ RequireComponent( typeof( Collider2D ) ) ]
    [ RequireComponent( typeof( Rigidbody2D ) ) ]
    public class PlayerShipController : MonoBehaviour, IDamageable, IPowerUpable
    {
        public static PlayerShipController Instance { private set; get; }
        public float ShipSpeed = 4.0f;
        public OneShotGun RocketGun;
        public OneShotGun LightningGun;

        private Animator _animator;
        private float _shipSizeToLeft;
        private float _shipSizeToRight;
        private float _shipSizeToBottom;
        private float _shipSizeToTop;

        private const float LightningGunDuration = 3f;
        private float _lighningGunTimeRemaining = 0f;
        private bool _isLighningGunActive;

        private void Awake()
        {
            InitializeSingleton();
            UpdateShipSize();
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            MoveShip();
            ProcessPowerUp();
        }

        private void OnEnable()
        {
            UpdateShipSize();
        }

        public void Die()
        {
            //TODO: TN> run some death animation
            //foreach( var weapon in _weapons )
            //    weapon.AutoFireStop();

            gameObject.SetActive( false );
        }

        public void UsePowerUp( PowerUpType powerUpType )
        {
            switch( powerUpType )
            {
                case PowerUpType.HealthPowerUp :
                    PlayerDataManager.Instance.Heal( 1 );
                    break;
                case PowerUpType.LightningGunPowerUp :
                    RocketGun.gameObject.SetActive( false );
                    LightningGun.gameObject.SetActive( true );
                    _lighningGunTimeRemaining = LightningGunDuration;
                    _isLighningGunActive = true;
                    break;
                default :
                    Debug.LogError( "trying to use unknown PowerUpType: " + powerUpType );
                    break;
            }
        }

        public void TakeDamage( int damage )
        {
            PlayerDataManager.Instance.TakeDamage( damage );
        }

        private void ProcessPowerUp()
        {
            if( !_isLighningGunActive )
                return;

            _lighningGunTimeRemaining -= Time.deltaTime;
            if( _lighningGunTimeRemaining <= 0f )
            {
                _isLighningGunActive = false;
                RocketGun.gameObject.SetActive( true );
                LightningGun.gameObject.SetActive( false );
            }
        }

        private void MoveShip()
        {
            var xFromJoystick = Input.GetAxisRaw( "Horizontal" );
            var xFromMouse = Mathf.Clamp( Input.GetAxisRaw( "Mouse X" ), -1f, 1f );
            var yFromJoystick = Input.GetAxisRaw( "Vertical" );
            var yFromMouse = Mathf.Clamp( Input.GetAxisRaw( "Mouse Y" ), -1f, 1f );

            var inputX = Mathf.Abs( xFromJoystick ) > Mathf.Abs( xFromMouse )
                ? xFromJoystick
                : xFromMouse;
            var inputY = Mathf.Abs( yFromJoystick ) > Mathf.Abs( yFromMouse )
                ? yFromJoystick
                : yFromMouse;
            var movementMultiplieer = ShipSpeed*Time.deltaTime;

            var x = inputX*movementMultiplieer;
            var y = inputY*movementMultiplieer;

            var xPos = Mathf.Clamp( transform.position.x + x, GameFieldBoundaryController.Instance.Xmin - _shipSizeToLeft,
                                    GameFieldBoundaryController.Instance.Xmax - _shipSizeToRight );
            var yPos = Mathf.Clamp( transform.position.y + y, GameFieldBoundaryController.Instance.Ymin - _shipSizeToBottom,
                                    GameFieldBoundaryController.Instance.Ymax - _shipSizeToTop );

            _animator.SetFloat( "HorizontalSpeed", xPos - transform.position.x );

            transform.position = new Vector2( xPos, yPos );
        }

        private void UpdateShipSize()
        {
            //NOTE: TN> method should be called if ship(collider) size changed. For example one wing got destroyed or ship got bigger.
            var shipColliderBounds = GetComponent<Collider2D>().bounds;

            var colliderOffsetToShip = shipColliderBounds.center - transform.position;
            _shipSizeToLeft = colliderOffsetToShip.x - shipColliderBounds.extents.x;
            _shipSizeToRight = colliderOffsetToShip.x + shipColliderBounds.extents.x;
            _shipSizeToBottom = colliderOffsetToShip.y - shipColliderBounds.extents.y;
            _shipSizeToTop = colliderOffsetToShip.y + shipColliderBounds.extents.y;
        }

        private void InitializeSingleton()
        {
            if( Instance != null )
            {
                Debug.LogError( "There is another instance of PlayerShipController on Awake. Must not happen." );
                Destroy( Instance.gameObject );
            }

            Instance = this;
        }
    }
}