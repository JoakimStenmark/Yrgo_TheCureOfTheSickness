using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    GameObject gameController;
    TunnelController tunnelController;
    PathManager pathManager;

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

        pathManager = GameObject.FindGameObjectWithTag(Tags.levelManager).GetComponent<PathManager>();

        //player = GameObject.FindGameObjectWithTag (Tags.player);
    }

    void Update() {

        transform.LookAt(player.transform);
        transform.position = pathManager.FollowPath("TunnelPath", transform.position, moveSpeed);
    }
}
