using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TargetReticule : MonoBehaviour
{

    public float distanceFromPlayer;
    public float maxDistanceFromPlayer;

    private float distanceBetweenPlayerAndCamera;
    private GameObject player;
    public float reticleDepth = 30;

    public LayerMask rayMask;

    RaycastHit hit;

    void Start()
    {
        player = transform.parent.gameObject;
    }

    void LateUpdate()
    {
        float playerZPosition = transform.parent.gameObject.transform.position.z;
        float cameraZPosition = Camera.main.transform.position.z;

        distanceBetweenPlayerAndCamera = playerZPosition - cameraZPosition;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000f,rayMask))
        {
            //TODO - no collision with player
            //Debug.Log(hit.collider.gameObject);
            reticleDepth = Mathf.Clamp(hit.distance, distanceFromPlayer, maxDistanceFromPlayer);            
        }
        else
        {
            reticleDepth = maxDistanceFromPlayer;
        }

        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, reticleDepth);
        
        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
           
    }
}
