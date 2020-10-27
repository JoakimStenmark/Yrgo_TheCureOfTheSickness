using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour {

    GameSettings gameSettings;
    GameObject gameController;

    [SerializeField]
    List<Path> pathList;

    public void Start() {

        gameController = GameObject.FindGameObjectWithTag(Tags.gameController);
        gameSettings = gameController.GetComponent<GameSettings>(); 
    }

    public void Update() {

        if( gameSettings.debug ) {

            foreach( Path path in pathList) {
             
                // Run though each vector3 in path here and render lines between
            }
        }
    }

    public Vector3 FollowPath( string pathName, Vector3 currentPosition, float speed ) {

        Vector3 returnValue = currentPosition;

        Path path = FindPath(pathName);

        if( path != null ) {

            Vector3 pathGoal = path.CurrentPathGoal( currentPosition );

            Debug.Log ("path goal: " + pathGoal);

            returnValue = Vector3.MoveTowards ( currentPosition, pathGoal, speed * Time.deltaTime );
            
            Debug.Log ("returnvalue: " + returnValue);
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

            Debug.LogError ("No path with the name " + pathName + " found!");
        }
    }
}
