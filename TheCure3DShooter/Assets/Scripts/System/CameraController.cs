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
    public float xySmoothSpeed;

    void Start() {

        gameController = GameObject.FindGameObjectWithTag (Tags.gameController);
        tunnelController = gameController.GetComponent<TunnelController>();

        pathManager = GameObject.FindGameObjectWithTag(Tags.levelManager).GetComponent<PathManager>();

        //player = GameObject.FindGameObjectWithTag (Tags.player);
    }

    void LateUpdate() {

        

        Vector3 pathPosition = pathManager.FollowPathSmooth("TunnelPath", transform.position, xySmoothSpeed);

        transform.position = new Vector3(pathPosition.x, pathPosition.y, transform.position.z + moveSpeed * Time.deltaTime);

       // Vector3 newPosition = pathManager.FollowPathSmooth("TunnelPath", transform.position, moveSpeed);

//         transform.position = new Vector3(
//         
//             (newPosition.x + player.transform.position.x) / 2,
//             (newPosition.y + player.transform.position.y) / 2,
//             newPosition.z
//             );
        //transform.LookAt(player.transform);
    }
}
