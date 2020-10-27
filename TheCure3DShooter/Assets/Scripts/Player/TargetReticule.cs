using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TargetReticule : MonoBehaviour
{

    public float distanceFromPlayer;
    private float distanceBetweenPlayerAndCamera;
    private GameObject player;

    void Start()
    {
        player = transform.parent.gameObject;
    }

    void LateUpdate()
    {
        float playerZPosition = transform.parent.gameObject.transform.position.z;
        float cameraZPosition = Camera.main.transform.position.z;

        distanceBetweenPlayerAndCamera = playerZPosition - cameraZPosition;

        Debug.Log(playerZPosition);


        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceBetweenPlayerAndCamera + distanceFromPlayer);
        
        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
           
    }
}
