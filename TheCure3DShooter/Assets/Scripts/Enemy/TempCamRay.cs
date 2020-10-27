using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCamRay : MonoBehaviour
{
    public Camera camera;
    public Transform mousePoint;
    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        if (camera == null)
            camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit rayHit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out rayHit))
        {
            mousePoint.position = rayHit.point;
            if (Input.GetButton("Fire1"))
            {
                Instantiate(projectile, transform.position + ray.direction, Quaternion.LookRotation(ray.direction));
            }

           // Transform objectHit = rayHit.transform;

            // Do something with the object that was hit by the raycast.
        }
    }
}
