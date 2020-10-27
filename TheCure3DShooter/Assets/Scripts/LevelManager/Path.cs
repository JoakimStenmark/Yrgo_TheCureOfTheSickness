using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Path {

    public string name;

    [SerializeField]
    List<Vector3> pathPoint = new List<Vector3>();

    public void AddPoint( Vector3 point ) {

        pathPoint.Add( point );
    }

    public Vector3 CurrentPathGoal( Vector3 currentPosition ) {
    
        Vector3 returnValue = currentPosition;

        for( int i = 0; i < pathPoint.Count; i++ ) {

            if( currentPosition.z < pathPoint[i].z ) {
                
                return pathPoint[i];
            }
        }
        
        return returnValue;    
    }

    void OnDrawGizmos() {

        Gizmos.color = Color.white;

        for( int i = 0; i < pathPoint.Count; i++ ) {

            Vector3 currentPosition = pathPoint[i];

            if( i > 0 ) {

                Vector3 lastPosition = pathPoint[i - 1];

                Gizmos.DrawLine (lastPosition, currentPosition);
                Gizmos.DrawSphere(currentPosition, 0.3f);
            }
        }
    }
}

