using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{

    public Transform player;
    public float distanceFromPlayer;
    private Vector3 positionWithOffset;
    

    private void Update()
    {
        
        positionWithOffset = new Vector3(transform.position.x, transform.position.y, player.position.z - distanceFromPlayer);

        transform.position = positionWithOffset;
        
        
    }
}
