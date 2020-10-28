// ROBIN B
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    GameObject player;

    [SerializeField]
    Vector3 pathFrom;

    [SerializeField]
    Vector3 pathGoal;

    public float moveSpeed;
    public float xySmoothSpeed;

    void LateUpdate() {

        Vector3 pathPosition = PathManager.instance.FollowPathSmooth( "TunnelPath", transform.position, xySmoothSpeed );

        transform.position = new Vector3( pathPosition.x, pathPosition.y, transform.position.z + moveSpeed * Time.deltaTime );
    }
}
