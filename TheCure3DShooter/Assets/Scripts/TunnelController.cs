using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelController : MonoBehaviour
{

    public GameObject tunnelPart;

    GameObject[] tunnelSegments;
    public Vector3[] navPoint;

    public int mapLength = 50;
    public float offset;
    public float zOffset;

    bool debug = true;

    void Start() {

        tunnelSegments = new GameObject[ mapLength ];
        tunnelSegments[0] = tunnelPart;

        navPoint = new Vector3[ mapLength ];

        GenerateMap();
        GeneratePath();
    }

    void Update() {

        if( debug ) {

            for( int i = 0; i < navPoint.Length; i++ ) {

                if( i < navPoint.Length - 1 ) {

                    Debug.DrawLine(navPoint[ i ], navPoint[ i + 1 ]);
                }
            }
        }
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
    }

    void GeneratePath() {

        for( int i = 0; i < navPoint.Length; i++ ) {

            navPoint[ i ] = tunnelSegments[ i ].transform.position; 
        }
    }
}
