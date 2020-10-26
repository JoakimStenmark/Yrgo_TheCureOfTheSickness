using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelController : MonoBehaviour {

    public bool tunnelGenerated = false;
    bool debug = true;

    public GameObject tunnelPart;

    public GameObject[] tunnelSegments;

    public int mapLength = 50;
    public float offset;
    public float zOffset;

    

    void Start() {

        tunnelSegments = new GameObject[ mapLength ];
        tunnelSegments[0] = tunnelPart;

        GenerateMap();
        
    }

    void GenerateMap() {

        Vector3 lastPosition;
        lastPosition = Vector3.zero;

        for( int i = 1; i < mapLength; i++ ) {

            Vector3 newPosition = new Vector3(

                Random.Range(lastPosition.x - offset, lastPosition.x + offset),
                Random.Range(lastPosition.y - offset, lastPosition.y + offset),
                i * zOffset
            );

            GameObject obj = Instantiate(tunnelPart, newPosition, Quaternion.identity);

            tunnelSegments[i] = obj;

            lastPosition = newPosition;
        }

        tunnelGenerated = true;
    }
}
