// ROBIN B
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour {

    public static PathManager instance;

    [SerializeField]
    List<Path> pathList = new List<Path>();

    void Start() {

        if( instance == null ) {

            instance = this;
        } else {

            Destroy(this.gameObject);
        }
    }

    void OnDrawGizmos() {

        foreach( Path path in pathList ) {

            for( int i = 0; i < path.pathPoint.Count; i++ ) {

                if( i > 0 ) {

                    if( GameManager.instance.debugActive ) {

                        Debug.DrawLine( path.pathPoint[ i - 1 ], path.pathPoint[ i ] );
                    }
                    Gizmos.DrawSphere( path.pathPoint[ i ], 0.5f );
                }
            }
        }
    }

    public Vector3 FollowPath( string pathName, Vector3 currentPosition, float speed ) {

        Vector3 returnValue = currentPosition;

        Path path = FindPath( pathName );

        if( path != null ) {

            Vector3 pathGoal = path.CurrentPathGoal( currentPosition );

            returnValue = Vector3.MoveTowards( currentPosition, pathGoal, speed * Time.deltaTime );
        }

        return returnValue;
    }

    public Vector3 FollowPathSmooth( string pathName, Vector3 currentPosition, float speed , float smoothSpeed ) {

        Vector3 returnValue = currentPosition;

        Path path = FindPath( pathName );

        if( path != null ) {

            Vector3 pathGoal = path.CurrentPathGoal( currentPosition );

            Vector2 xyTarget = new Vector2( pathGoal.x, pathGoal.y );
            Vector2 xyCurrent = new Vector2( currentPosition.x, currentPosition.y );

            Vector2 xyVector = Vector2.MoveTowards( xyCurrent, xyTarget, smoothSpeed * Time.deltaTime );

            pathGoal = new Vector3(xyVector.x, xyVector.y, pathGoal.z);

            returnValue = Vector3.MoveTowards( currentPosition, pathGoal, speed * Time.deltaTime );
        }

        return returnValue;
    }

    public Vector3 FollowPathSmooth( string pathName, Vector3 currentPosition, float smoothSpeed ) {

        Vector3 returnValue = currentPosition;

        Path path = FindPath( pathName );

        if( path != null ) {

            Vector3 pathGoal = path.CurrentPathGoal( currentPosition );

            Vector2 xyTarget = new Vector2( pathGoal.x, pathGoal.y );
            Vector2 xyCurrent = new Vector2( currentPosition.x, currentPosition.y );

            Vector2 xyVector = Vector2.MoveTowards( xyCurrent, xyTarget, smoothSpeed * Time.deltaTime );

            returnValue = new Vector3( xyVector.x, xyVector.y, currentPosition.z );
        }

        return returnValue;
    }

    public Vector3 FollowPathSmoothBetween( string pathName, Vector3 currentPosition, float smoothSpeed ) {

        Vector3 returnValue = currentPosition;

        Path path = FindPath( pathName );

        if( path != null ) {

            Vector3 pathGoal = path.CurrentPathGoal( currentPosition );
            Vector3 pathLastGoal = path.LastPathGoal( currentPosition );

            Vector2 xyTarget = new Vector2( pathGoal.x, pathGoal.y );
            Vector2 xyCurrent = new Vector2( currentPosition.x, currentPosition.y );
            Vector2 xyLastTarget = new Vector2( pathLastGoal.x, pathLastGoal.y );
            float positionIndex = Mathf.InverseLerp( pathLastGoal.z, pathGoal.z, currentPosition.z );

            Vector2 xyNewPosition = Vector2.Lerp( xyLastTarget, xyTarget, positionIndex );

            returnValue = new Vector3( xyNewPosition.x, xyNewPosition.y, currentPosition.z );
        }

        return returnValue;
    }

    public void NewPath( string pathName ) {

        Path newPath = new Path();
        newPath.name = pathName;
        pathList.Add( newPath );
    }

    Path FindPath( string pathName ) {

        Path returnValue = null;

        foreach( Path path in pathList ) {

            if( path.name == pathName ) {

                return path;
            }
        }

        return returnValue;
    }

    public void AddPoint( string pathName, Vector3 position ) {

        Path path = FindPath( pathName );

        if( path == null ) {

            Debug.LogError( "ADD POINT : No path with the name " + pathName + " found!" );
            return;
        }

        path.AddPoint( position );
    }

    public int GetPathLength( string pathName ) {

        Path path = FindPath( pathName );

        if( path == null ) {

            Debug.LogError( "GET PATH LENGTH : No path with the name " + pathName + " found!" );
            return 0;
        }

        return path.pathPoint.Count;
    }

    public Vector3 GetPointPosition( string pathName, int index ) {

        Path path = FindPath( pathName );

        if( path == null ) {

            Debug.LogError( "GET POINT POSITION : No path with the name " + pathName + " found!" );
            return Vector3.zero;
        }

        return path.pathPoint[ index ];
    }
}
