using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour {

    GameSettings gameSettings;
    GameObject gameController;

    [SerializeField]
    List<Path> pathList;

    public float smoothSpeed;

    public void Start() {

        gameController = GameObject.FindGameObjectWithTag(Tags.gameController);
        gameSettings = gameController.GetComponent<GameSettings>();
    }

    public void Update() {

        if( gameSettings.debug ) {

            foreach( Path path in pathList ) {

                for( int i = 0; i < path.pathPoint.Count; i++ ) {

                    if( i > 0 ) {

                        Debug.DrawLine(path.pathPoint[ i - 1 ], path.pathPoint[ i ]);
                    }
                }
            }
        }
    }

    public Vector3 FollowPath( string pathName, Vector3 currentPosition, float speed ) {

        Vector3 returnValue = currentPosition;

        Path path = FindPath(pathName);

        if( path != null ) {

            Vector3 pathGoal = path.CurrentPathGoal(currentPosition);

            returnValue = Vector3.MoveTowards(currentPosition, pathGoal, speed * Time.deltaTime);
        }

        return returnValue;
    }

    public Vector3 FollowPathSmooth( string pathName, Vector3 currentPosition, float speed ) {

        Vector3 returnValue = currentPosition;

        Path path = FindPath(pathName);

        if( path != null ) {

            Vector3 pathGoal = path.CurrentPathGoal(currentPosition);

            float distance = Vector3.Distance(currentPosition, pathGoal);

            Vector2 xyTarget = new Vector2(pathGoal.x, pathGoal.y);
            Vector2 xyCurrent = new Vector2(currentPosition.x, currentPosition.y);

            float step = smoothSpeed * Time.deltaTime;

            Vector2 xyVector = Vector2.MoveTowards(xyCurrent, xyTarget, step);

            pathGoal = new Vector3(xyVector.x, xyVector.y, pathGoal.z);

            returnValue = Vector3.MoveTowards(currentPosition, pathGoal, speed * Time.deltaTime);
        }

        return returnValue;
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

    public void NewPath( string pathName ) {

        Path newPath = new Path();
        newPath.name = pathName;
        pathList.Add(newPath);
    }

    public void AddPoint( string pathName, Vector3 position ) {

        Path tempPath = null;

        foreach( Path path in pathList ) {

            if( path.name == pathName ) {

                tempPath = path;
                tempPath.AddPoint(position);
                break;
            }
        }

        if( tempPath == null ) {

            Debug.LogError("No path with the name " + pathName + " found!");
        }
    }
}
