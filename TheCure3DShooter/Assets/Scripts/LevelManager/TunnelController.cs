using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelController : MonoBehaviour {

    GameSettings gameSettings;

    public int tunnelLength = 10;

    public bool tunnelGenerated = false;

    public GameObject tunnelPart;

    public GameObject[] tunnelSegments;

    public float offset;
    public float zOffset;

    void Start() {

        gameSettings = GetComponent<GameSettings>();

        tunnelSegments = new GameObject[ tunnelLength ];
        tunnelSegments[0] = tunnelPart;

        GenerateMap();
    }

    void GenerateMap() {

        Vector3 lastPosition;
        lastPosition = Vector3.zero;

        for( int i = 1; i < tunnelLength; i++ ) {

            Vector3 newPosition = new Vector3(

                Random.Range(lastPosition.x - offset, lastPosition.x + offset),
                Random.Range(lastPosition.y - offset, lastPosition.y + offset),
                i * zOffset
            );


            tunnelSegments[ i ] = Instantiate(tunnelPart, newPosition, Quaternion.identity);

            lastPosition = newPosition;
        }
    }
}
