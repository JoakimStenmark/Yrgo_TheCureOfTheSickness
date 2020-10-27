using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    GameSettings gameSettings;
    LevelObjectPool levelObjectPool;
    PathManager pathManager;

    GameObject player;

    public GameObject[] levelParts; 

    public int tunnelLength;

    public float xyOffset;
    public float zOffset;

    public Vector3[] tunnelSegments;

    void Start() {
        
        gameSettings = GetComponent<GameSettings>();
        levelObjectPool = GetComponent<LevelObjectPool>();
        pathManager = GetComponent<PathManager>();

        player = GameObject.FindGameObjectWithTag( Tags.player );

        tunnelSegments = new Vector3[ tunnelLength ];

        GenerateTunnel();

    }

    public void GenerateTunnel() {

        pathManager.NewPath("TunnelPath");

        Vector3 lastPosition;
        lastPosition = Vector3.zero;

        for( int i = 1; i < tunnelLength; i++ ) {

            Vector3 newPosition = new Vector3(

                Random.Range(lastPosition.x - xyOffset, lastPosition.x + xyOffset),
                Random.Range(lastPosition.y - xyOffset, lastPosition.y + xyOffset),
                i * zOffset
            );


            tunnelSegments[ i ] = newPosition;
            pathManager.AddPoint("TunnelPath", newPosition);
            lastPosition = newPosition;
        }
    }
}
