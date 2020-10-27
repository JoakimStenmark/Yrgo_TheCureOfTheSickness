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

    public void NewPath( string name ) {

        Path newPath = new Path();
        newPath.name = name;
        pathList.Add(newPath);
    }

    public void AddPoint( string name, Vector3 position ) {

        Path tempPath = null;

        foreach( Path path in pathList ) {

            if( path.name == name ) {
                
                tempPath = path;
                tempPath.AddPoint(position);
                break;
            }
        }

        if( tempPath == null ) {

            Debug.LogError ("No path with the name " + name + " found!");
        }
    }
}
