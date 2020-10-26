using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour {

    GameSettings gameSettings;
    TunnelController tunnelController;

    public Vector3[] navPoint;

    bool navPointsGenerated = false;

    void Start() {

        gameSettings = GetComponent<GameSettings>();
        tunnelController = GetComponent<TunnelController>();

        navPoint = new Vector3[ gameSettings.mapLength ];
    }

    void Update() {

        if( navPointsGenerated == false ) {

            if( tunnelController.tunnelGenerated ) {

                GenerateNavPoints();
            }
        } else {

            if( gameSettings.debug ) {

                for( int i = 0; i < navPoint.Length; i++ ) {

                    if( i < navPoint.Length - 1 ) {

                        Debug.DrawLine(navPoint[ i ], navPoint[ i + 1 ]);
                    }
                }
            }
        }
    }

    void GenerateNavPoints() {

        for( int i = 0; i < navPoint.Length; i++ ) {

            navPoint[ i ] = tunnelController.tunnelSegments[ i ].transform.position;
        }

        navPointsGenerated = true;
    }
}
