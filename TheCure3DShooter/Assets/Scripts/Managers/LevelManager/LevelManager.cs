// ROBIN B
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    
    public static LevelManager instance;

    //List<GameObject> ObjectList = new List<GameObject>();

    GameObject player;

    [Header("Prefabs")]
    public GameObject tunnelPart;
    [Space(10)]
    public GameObject spotLightPrefab;
    public GameObject enemySpawnerPrefab;
    public GameObject enemyAnchorFollowerPrefab;
    public GameObject goalTriggerPrefab;

    public bool spawnLights;
    public bool spawnEnemies;

    public int tunnelLength;
    public int pathPointStep = 5;
    public int enemySpawnFrequency = 50;
    public int spawnFrequencyModifier = 10;

    public float xyOffset;
    public float zOffset;

    public GameObject[] tunnelSegments;

    void Start() {

        if( instance == null ) {

            instance = this;
        } else {

            Destroy(this.gameObject);
        }

        player = GameObject.FindGameObjectWithTag( Tags.player );

        tunnelSegments = new GameObject[ tunnelLength ];

        GenerateLevel();

        GameManager.instance.ChangeGameState( GameManager.GameState.GameLoop );
    }

    public void GenerateLevel() {

        PathManager.instance.NewPath( "TunnelPath" );

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

                PathManager.instance.AddPoint( "TunnelPath", newPosition );
            }

            if( i % pathPointStep == 0 ) {

                PathManager.instance.AddPoint( "TunnelPath", newPosition );
            }

            if( i % enemySpawnFrequency == 0 && i != 0 && enemySpawnerPrefab != null && spawnEnemies ) {

                CreateNewObject(enemySpawnerPrefab, newPosition, false).GetComponent<EnemyCluster>().RandomizeSpawnAtLevel(i);
                //AddObject( enemySpawnerPrefab, newPosition ).GetComponent<EnemyCluster>().railAnchor = enemyAnchorFollowerPrefab;

                if( enemySpawnFrequency > 1 && i % spawnFrequencyModifier == 0 ) {

                    enemySpawnFrequency--;
                }
            }

            if( i == tunnelLength - 1 ) {

                CreateNewObject( goalTriggerPrefab, newPosition, true );
            }

            lastPosition = newPosition;
        }
    }

    GameObject CreateNewObject( GameObject objectToAdd, Vector3 position, bool setAsChild ) {

        GameObject newObject;

        newObject = Instantiate( objectToAdd, position, Quaternion.identity );

        if( setAsChild ) {

            newObject.transform.parent = transform;
        }

        return newObject;
    }
}
