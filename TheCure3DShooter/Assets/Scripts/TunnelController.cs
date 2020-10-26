using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelController : MonoBehaviour
{

    public GameObject[] tunnelSegments;

    public int mapLength = 50;
    public float offset;

    void Start() {
        
        GenerateMap();
    }

    void Update() {
        
        
    }

    void GenerateMap() {

        Vector3 lastPosition;
        lastPosition = Vector3.zero;

        for( int i = 1; i < mapLength; i++ ) {

            Vector3 newPosition = new Vector3(

                Random.Range(lastPosition.x - offset, lastPosition.x + offset),
                Random.Range(lastPosition.y - offset, lastPosition.y + offset),
                i
            );

            Instantiate (tunnelSegments[0], newPosition, Quaternion.identity);

            lastPosition = newPosition;
        }
    }
}
