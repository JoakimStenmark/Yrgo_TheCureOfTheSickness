using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TargetReticule : MonoBehaviour
{

    public float distanceFromPlayer;
    public float distanceBetweenPlayerAndCamera;

    void Start()
    {
        
    }

    void Update()
    {
        distanceBetweenPlayerAndCamera = GetComponentInParent<Transform>().position.z - Camera.main.transform.position.z;

        Debug.Log(GetComponentInParent<Transform>().position.z);

        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceFromPlayer);
        
        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
           
    }
}
