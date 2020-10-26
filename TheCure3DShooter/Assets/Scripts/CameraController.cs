using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour{

    [SerializeField]
    GameObject player;

    [SerializeField]
    Vector3 pathFrom;

    [SerializeField]
    Vector3 pathGoal;

    public float moveSpeed;
    float lerpStep = 0;

    void Start() {

        player = GameObject.FindGameObjectWithTag (Tags.player);
    }

    void Update() {

        if( lerpStep < 1 ) {
            
            lerpStep += moveSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp (pathFrom, pathGoal, lerpStep);
        }

        // Input for testing, remove later...
        if( Input.GetKeyDown(KeyCode.Space) ) {

            SetPathGoal (new Vector3(Random.Range (0, 1000), Random.Range (0, 1000), Random.Range (0, 1000)));
        }
    }

    public void SetPathGoal( Vector3 goal ) {

        pathFrom = transform.position;
        pathGoal = goal;
        lerpStep = 0;
    }
}
