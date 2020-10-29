// ROBIN B
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Path {

    public string name;

    [SerializeField]
    public List<Vector3> pathPoint = new List<Vector3>();

    public void AddPoint( Vector3 point ) {

        pathPoint.Add( point );
    }

    public Vector3 CurrentPathGoal( Vector3 currentPosition ) {
    
        Vector3 returnValue = currentPosition;

        for( int i = 0; i < pathPoint.Count; i++ ) {

            if( currentPosition.z < pathPoint[ i ].z ) {
                
                return pathPoint[ i ];
            }
        }
        
        return returnValue;    
    }

    public Vector3 LastPathGoal( Vector3 currentPosition ) {

        Vector3 returnValue = Vector3.zero;

        for( int i = 0; i < pathPoint.Count; i++ ) {

            if( currentPosition.z < pathPoint[ i ].z && i > 1 ) {

                return pathPoint[ i - 1 ];
            }
        }
    
        return returnValue;
    }
}

