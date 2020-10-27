using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float directionalSpeed;
    [SerializeField]
    private float forwardSpeed;
 
    Rigidbody rb;

    public Transform targetingReticule;

    RaycastHit hit;

    [Header("Weapon")]
    public int clipSize = 1;
    public GameObject laserShotPrefab;
    GameObject[] laserShots;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        laserShots = new GameObject[clipSize];

        for (int i = 0; i < laserShots.Length; i++)
        {
            laserShots[i] = Instantiate(laserShotPrefab);
        }
        
    }

    void Update()
    {
        transform.LookAt(targetingReticule.position);

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }


    void FixedUpdate()
    {
        Movement();
    }
    private void Shoot()
    {
        //fire laser
        Debug.DrawRay(transform.position, transform.forward, Color.red, 100f);

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            Debug.Log("hit a " + hit.collider.gameObject.name);
        }

        foreach (GameObject laserShot in laserShots)
        {
            if (!laserShot.activeSelf)
            {
                laserShot.SetActive(true);
                break;
            }
        }

    }

    void Movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical);

        if (movement.sqrMagnitude > 1)
        {
            movement.Normalize();
        }

        rb.MovePosition(new Vector3(rb.position.x, rb.position.y, rb.position.z + forwardSpeed));
        Vector3 thrust = new Vector3(0, 0, forwardSpeed);

        rb.AddForce(movement * directionalSpeed);
        //rb.AddForce(thrust);
    }

}
