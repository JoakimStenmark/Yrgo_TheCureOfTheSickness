using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceMoveEnemy : MonoBehaviour
{

    public Vector3 target;
    private Rigidbody rb;

    public float straitenSpeed = 10;
    public float speed = 10;


    public Vector3[] patrolPath;
    private int p = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            rb.AddForce(Vector3.forward * 100);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if(patrolPath.Length > 1)
        {
            target = patrolPath[p];
        }

        Vector3 dir = target - transform.position;
        if(dir.sqrMagnitude < 2)
        {
            p++;
            if (p >= patrolPath.Length)
                p = 0;
        }


        Vector3 velo = rb.velocity;
        float dot = Vector3.Dot(dir, velo);
        dot += 1;
        dot = 3 - dot;
        dot *= 2;
        rb.AddForce(dir * speed * dot * Time.deltaTime);

        //Debug
        Debug.DrawRay(transform.position, dir * 10 * dot, Color.green);
        for (int i = 0; i < patrolPath.Length; i++)
        {
            Debug.DrawRay(patrolPath[i], Vector3.one, Color.blue);
        }
    }
}
