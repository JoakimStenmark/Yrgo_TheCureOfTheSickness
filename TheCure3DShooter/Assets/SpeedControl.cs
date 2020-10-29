using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedControl : MonoBehaviour {

    public float speedIncrease;
    public float speedDecrease;
    public float maxSpeed;

    private float baseSpeed;

    PathFollower pathFollower;

    void Start() {
        
        pathFollower = GetComponent<PathFollower>();
        baseSpeed = pathFollower.moveSpeed;
    }

    void Update() {

        if( Input.GetMouseButton( 1 )) {

            if( pathFollower.moveSpeed < maxSpeed ) {

                pathFollower.moveSpeed += speedIncrease * Time.deltaTime;
            }
        } else if( pathFollower.moveSpeed > baseSpeed ) {

            pathFollower.moveSpeed -= speedDecrease * Time.deltaTime;
        }
    }

    void OnMouseDown() {


    }
}
