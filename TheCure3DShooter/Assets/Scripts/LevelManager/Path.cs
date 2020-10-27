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
}
