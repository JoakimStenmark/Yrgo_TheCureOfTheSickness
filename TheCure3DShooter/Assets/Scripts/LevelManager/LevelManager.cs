using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    List<GameObject> ObjectList = new List<GameObject>();

    GameSettings gameSettings;
    PathManager pathManager;

    GameObject player;

    public GameObject tunnelPart;
    public GameObject lightPart;
    public GameObject enemySpawner;
    public GameObject bloodCell;
    public GameObject enemyAnchorFollower;
    public GameObject pillarObject;

    public bool spawnLights;
    public bool spawnEnemies;
    public bool spawnBloodcells;

    public int tunnelLength;
    public int pathPointStep = 5;
    public int enemySpawnFrequency = 50;
    public int pillarFrequency = 30;

    public float xyOffset;
    public float zOffset;

    public GameObject[] tunnelSegments;

    void Start() {
        
        gameSettings = GetComponent<GameSettings>();
        pathManager = GetComponent<PathManager>();

        player = GameObject.FindGameObjectWithTag( Tags.player );

        tunnelSegments = new GameObject[ tunnelLength ];

        GenerateLevel();
    }

    public void GenerateLevel() {

        pathManager.NewPath( "TunnelPath" );

        Vector3 lastPosition;
        lastPosition = Vector3.zero;

        for( int i = 0; i < tunnelLength; i++ ) {

            Vector3 newPosition = new Vector3(

                Random.Range( lastPosition.x - xyOffset, lastPosition.x + xyOffset ),
                Random.Range( lastPosition.y - xyOffset, lastPosition.y + xyOffset ),
                i * zOffset
            );

            tunnelSegments[ i ] = Instantiate( tunnelPart, newPosition, Quaternion.identity );
            tunnelSegments[ i ].transform.parent = transform;

            if( i == 0 ) {

                pathManager.AddPoint( "TunnelPath", newPosition );
            }

            if( i % pathPointStep == 0 ) {

                pathManager.AddPoint( "TunnelPath", newPosition );

                if( spawnBloodcells ) {

                    CreateNewObject(bloodCell, newPosition);
                }
            }

            if( i % pillarFrequency == 0 ) {

                SpawnPillars( newPosition );
            }

            if( i % enemySpawnFrequency == 0 && i != 0 && enemySpawner != null && spawnEnemies ) {

                CreateNewObject(enemySpawner, newPosition).GetComponent<EnemyCluster>().RandomizeSpawnAtLevel(i);
                //AddObject( enemySpawner, newPosition ).GetComponent<EnemyCluster>().railAnchor = enemyAnchorFollower;
            }

            lastPosition = newPosition;
        }
    }

    void SpawnPillars( Vector3 position ) {

        GameObject newObject;

        newObject = CreateNewObject(pillarObject, position);

        newObject.transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
        newObject.transform.Rotate(new Vector3(0, Random.Range(0, 360), 0));

    }

    GameObject CreateNewObject( GameObject objectToAdd, Vector3 position ) {

        GameObject newObject;

        newObject = Instantiate( objectToAdd, position, Quaternion.identity );

        ObjectList.Add( newObject );

        return newObject;
    }
}
