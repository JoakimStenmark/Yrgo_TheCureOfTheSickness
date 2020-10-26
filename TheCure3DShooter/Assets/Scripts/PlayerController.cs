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
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        

    }

    void Update()
    {
        //float x = Input.GetAxis("Horizontal");
        //float y = Input.GetAxis("Vertical");

        //Vector2 movement = new Vector2(x, y);
        //if (movement.sqrMagnitude > 1)
        //{
        //    movement = movement.normalized;
        //}       

        //rb.velocity = movement * speed * Time.deltaTime;
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        rb.MovePosition(new Vector3(rb.position.x, rb.position.y, rb.position.z + forwardSpeed));
        
        rb.AddForce(movement * directionalSpeed);
    }

}
