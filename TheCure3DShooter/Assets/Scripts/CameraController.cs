using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour{

    GameObject player;

    Vector3 pathFrom;
    Vector3 pathGoal;

    float moveSpeed = 10;
    float lerpStep = 0;

    void Start() {

        player = GameObject.FindGameObjectWithTag (Tags.player);
    }

    void Update() {

        if( lerpStep < 1 ) {
            
            lerpStep += moveSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp (pathFrom, pathGoal, lerpStep);
        }
    }

    public void SetPathGoal( Vector3 goal ) {

        pathFrom = transform.position;
        pathGoal = goal;
        lerpStep = 0;
    }
}
