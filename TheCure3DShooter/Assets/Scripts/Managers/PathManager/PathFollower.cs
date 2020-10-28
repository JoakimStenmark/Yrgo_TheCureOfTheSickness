using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour {

    public float smoothValue = 0.9f;
    public float moveSpeed = 25;

    void Update() {

        Vector3 pathPosition = PathManager.instance.FollowPathSmooth("TunnelPath", transform.position, smoothValue);

        transform.position = new Vector3( pathPosition.x, pathPosition.y, transform.position.z + moveSpeed * Time.deltaTime );
    }
}
