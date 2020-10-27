using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    GameSettings gameSettings;
    PathManager pathManager;

    GameObject player;

    public GameObject tunnelPart; 
    public GameObject enemySpawner;

    public int tunnelLength;
    public int pathPointStep = 5;

    public float xyOffset;
    public float zOffset;

    public GameObject[] tunnelSegments;

    void Start() {
        
        gameSettings = GetComponent<GameSettings>();
        pathManager = GetComponent<PathManager>();

        player = GameObject.FindGameObjectWithTag( Tags.player );

        tunnelSegments = new GameObject[ tunnelLength ];

        GenerateTunnel();
    }

    public void GenerateTunnel() {

        pathManager.NewPath("TunnelPath");

        Vector3 lastPosition;
        lastPosition = Vector3.zero;

        for( int i = 0; i < tunnelLength; i++ ) {

            Vector3 newPosition = new Vector3(

                Random.Range(lastPosition.x - xyOffset, lastPosition.x + xyOffset),
                Random.Range(lastPosition.y - xyOffset, lastPosition.y + xyOffset),
                i * zOffset
            );


            tunnelSegments[ i ] = Instantiate(tunnelPart, newPosition, Quaternion.identity);
            tunnelSegments[ i ].transform.parent = transform;

            if( i == 0 ) {

                pathManager.AddPoint("TunnelPath", newPosition);
            }

            if( i % pathPointStep == 0 ) {

                pathManager.AddPoint("TunnelPath", newPosition);
                Instantiate(enemySpawner, newPosition, Quaternion.identity);
            }

            lastPosition = newPosition;
        }
    }
}
