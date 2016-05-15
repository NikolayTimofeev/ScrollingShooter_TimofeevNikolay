using System.Collections.Generic;
using Assets.ScrollingShooter.Scripts;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public List<GameObject> Sprites = new List<GameObject>();

    public float FirstLayerSpeed = 0.03f;
    public float SecondLayerSpeed = 0.02f;
    public float ThirdLayerSpeed = 0.01f;

    [ Range( 0.0f, 1.0f ) ]
    public float SecondsBetweenStarsSpawns;

    private float _cooldown;

    private GameObject _firstLayer;
    private GameObject _secondLayer;
    private GameObject _thirdLayer;

    private void Awake()
    {
        _firstLayer = (GameObject)Instantiate( new GameObject(), transform.position, transform.rotation );
        _firstLayer.transform.parent = transform;
        _firstLayer.name = "FirstLayer";
        _secondLayer = (GameObject)Instantiate( new GameObject(), transform.position, transform.rotation );
        _secondLayer.transform.parent = transform;
        _secondLayer.name = "SecondLayer";
        _thirdLayer = (GameObject)Instantiate( new GameObject(), transform.position, transform.rotation );
        _thirdLayer.transform.parent = transform;
        _thirdLayer.name = "ThirdLayer";
    }

    private void Start()
    {
        FillBackground();
    }

    private void FillBackground()
    {
        for( int i = 0; i < 3000; i++ )
            Update();
    }

    private void Update()
    {
        if( _cooldown > 0f )
            _cooldown -= Time.deltaTime;

        Spawn();

        MoveBackgroundLayerObjects( _firstLayer, FirstLayerSpeed );
        MoveBackgroundLayerObjects( _secondLayer, SecondLayerSpeed );
        MoveBackgroundLayerObjects( _thirdLayer, ThirdLayerSpeed );
    }

    private void MoveBackgroundLayerObjects(
        GameObject layer,
        float speed )
    {
        foreach( Transform child in layer.transform )
        {
            child.position = new Vector2( child.position.x, child.position.y - speed*Time.deltaTime );
            if( child.position.y < GameFieldBoundaryController.Instance.Ymin )
                Destroy( child.gameObject );
        }
    }

    private void Spawn()
    {
        if( _cooldown > 0f )
            return;

        var yCoordianteToSpawn = GameFieldBoundaryController.Instance.Ymax +
                                 ( GameFieldBoundaryController.Instance.Ymax - GameFieldBoundaryController.Instance.Ymin )/10;

        var xCoordinateToSpawn = Random.Range( GameFieldBoundaryController.Instance.Xmin,
                                               GameFieldBoundaryController.Instance.Xmax );
        var rotationOverZ = Random.Range( -180f, 180f );
        var starGameObject =
            (GameObject)
                Instantiate( PickRandomSprite(), new Vector2( xCoordinateToSpawn, yCoordianteToSpawn ),
                             Quaternion.Euler( 0, 0, 180 + rotationOverZ ) );
        var layerToSpawn = Random.Range( 1, 4 );
        switch( layerToSpawn )
        {
            case 1 :
                starGameObject.transform.parent = _firstLayer.transform;
                starGameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
                break;
            case 2 :
                starGameObject.transform.parent = _secondLayer.transform;
                starGameObject.GetComponent<SpriteRenderer>().sortingOrder = -2;
                break;
            case 3 :
                starGameObject.transform.parent = _thirdLayer.transform;
                starGameObject.GetComponent<SpriteRenderer>().sortingOrder = -3;
                break;
            default :
                Debug.LogError( "Trying to spawn a star on unknown layer: " + layerToSpawn );
                break;
        }

        var animator = starGameObject.GetComponent<Animator>();
        animator.Play( 0, -1, Random.value );

        _cooldown = SecondsBetweenStarsSpawns;
    }

    private GameObject PickRandomSprite()
    {
        var randomIndex = Random.Range( 0, Sprites.Count );
        return Sprites[randomIndex];
    }
}