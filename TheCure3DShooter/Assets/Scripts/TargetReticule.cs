using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TargetReticule : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        
    }
}
