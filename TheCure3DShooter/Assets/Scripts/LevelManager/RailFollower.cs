using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailFollower : MonoBehaviour {

    PathManager pathManager;

    public float smoothValue = 0.9f;
    public float moveSpeed = 25;

    void Start() {
        
        pathManager = GameObject.FindGameObjectWithTag( Tags.levelManager ).GetComponent<PathManager>();
    }

    void Update() {

        Vector3 pathPosition = pathManager.FollowPathSmooth( "TunnelPath", transform.position, smoothValue );

        transform.position = new Vector3( pathPosition.x, pathPosition.y, transform.position.z + moveSpeed * Time.deltaTime );
    }
}
