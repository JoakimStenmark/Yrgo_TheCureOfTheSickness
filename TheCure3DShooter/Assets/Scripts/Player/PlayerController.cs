using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float directionalSpeed;
    [SerializeField]
    private float forwardSpeed;
 
    Rigidbody rb;

    public Transform targetingReticule;

    RaycastHit hit;

    public int clipSize = 1;
    GameObject laserShot;
    GameObject[] laserShots;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        laserShots = new GameObject[clipSize];
        foreach (GameObject laserShot in laserShots)
        {
            //laserShot = 
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
    }

    void Movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        if (movement.sqrMagnitude > 1)
        {
            movement.Normalize();
        }
        
        rb.MovePosition(new Vector3(rb.position.x, rb.position.y, rb.position.z + forwardSpeed));
        
        rb.AddForce(movement * directionalSpeed);

    }

}
