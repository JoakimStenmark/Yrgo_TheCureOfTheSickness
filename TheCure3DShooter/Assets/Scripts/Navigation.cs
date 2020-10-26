using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour {

    public Vector3[] navPoint;

    int mapLength = 500;
    bool debug = true;

    GameSettings gameSettings;
    TunnelController tunnelController;

    void Start() {

        gameSettings = GetComponent<GameSettings>();
        tunnelController = GetComponent<TunnelController>();

        navPoint = new Vector3[ gameSettings.mapLenth ];

        GeneratePath();
    }

    void Update() {

        if( gameSettings.debug ) {

            for( int i = 0; i < navPoint.Length; i++ ) {

                if( i < navPoint.Length - 1 ) {

                    Debug.DrawLine(navPoint[ i ], navPoint[ i + 1 ]);
                }
            }
        }
    }

    void GeneratePath() {

        for( int i = 0; i < navPoint.Length; i++ ) {

            navPoint[ i ] = tunnelController.tunnelSegments[ i ].transform.position;
        }
    }
}
