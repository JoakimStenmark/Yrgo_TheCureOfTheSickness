using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjectPool : MonoBehaviour {

    LevelManager levelManager;

    List<GameObject> objectPool = new List<GameObject>();

    void Start() {

        levelManager = GetComponent<LevelManager>();
    }

    public GameObject GetFreeObject( string name ) {

        GameObject returnObject = null;

        foreach( GameObject obj in objectPool ) {

            if( obj.activeSelf == false && obj.name == name ) {

                returnObject = obj;
            }
        }

        if( returnObject == null ) {

            foreach( GameObject obj in levelManager.levelParts ) {

                if( obj.name == name) {

                    returnObject = Instantiate (obj);
                    objectPool.Add (returnObject);
                }
            }
        }

        return returnObject;
    }
}
