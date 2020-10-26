using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour{

    GameObject gameController;
    TunnelController tunnelController;

    [SerializeField]
    GameObject player;

    [SerializeField]
    Vector3 pathFrom;

    [SerializeField]
    Vector3 pathGoal;

    public float moveSpeed;
    float lerpStep = 0;

    int pathStep = 0;

    void Start() {

        gameController = GameObject.FindGameObjectWithTag (Tags.gameController);
        tunnelController = gameController.GetComponent<TunnelController>();

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
