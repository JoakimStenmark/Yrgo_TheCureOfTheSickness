using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBy : MonoBehaviour {

    public Vector3 spinSpeed;
    public float resetDistance;
    public Vector3 resetDestination;
    public float resetDestinationOffset;

    void Start() {

        ResetPosition();
    }

    void Update() {


        transform.Rotate(spinSpeed);

        if( transform.position.z > resetDistance ) {

            ResetPosition();
        }
    }

    void ResetPosition() {

        transform.position = new Vector3(

            Random.Range(resetDestination.x - resetDestinationOffset, resetDestination.x + resetDestinationOffset),
            Random.Range(resetDestination.y - resetDestinationOffset, resetDestination.y + resetDestinationOffset),
            Random.Range(resetDestination.z - resetDestinationOffset, resetDestination.z + resetDestinationOffset)
        );
    }
}
