//Robert S
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class TempCamRay : MonoBehaviour
{
    public Camera camera;
    public Transform mousePoint;
    public GameObject projectile;

    public GameObject player;
    public float maxD = 5;

    public LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        if (camera == null)
            camera = Camera.main;

        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit rayHit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction, Color.red);

        if (Physics.Raycast(ray.origin, ray.direction, out rayHit, 1000, mask))
        {
            // mousePoint.position = rayHit.point;
            Vector3 dir = rayHit.point - player.transform.position;
            if(dir.sqrMagnitude > maxD * maxD)
            {
                dir = dir.normalized * maxD;
            }

            mousePoint.position = player.transform.position + dir;
            mousePoint.up = dir;

            if (Input.GetButton("Fire1"))
            {
                Instantiate(projectile, transform.position + ray.direction, Quaternion.LookRotation(ray.direction));
            }
        }
    }
}
