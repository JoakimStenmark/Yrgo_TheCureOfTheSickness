//Robert S
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    private GameObject railAnchor;
    private Vector2[] patrolPath;
    private int p = 0;

    [Header("FX")]
    public GameObject fxOnDeath;

    [Header("Behavior")]
    public float moveForwardAt = 2;
    public float dropFrorwardAt = 5;
    public float moveSpeed = 10;
    public bool homing = true;
    public float aimTime = 2;
    private bool chase = false;
    private float homingSpeed = 2;
    public float killAt = 20;

    public void SpawnInit(int spawNumber, Vector2[] path, GameObject ranchor, GameObject pl)
    {
        p = spawNumber;
        patrolPath = path;
        railAnchor = ranchor;
        player = pl;

        transform.position = new Vector3(patrolPath[p].x, patrolPath[p].y, transform.position.z);
    }
    private void OnCollisionEnter(Collision collision)
    {
        KillMe();
    }

    public void OnHit(int dmg)
    {
        KillMe();
    }
    public void KillMe()
    {
        Instantiate(fxOnDeath, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (chase)
        {
            HomingOnPlayer();
        }
        else
        {
            ForwardMovement();
            Patrol();
        }
    }

    void HomingOnPlayer()
    {

        if (homing)
        {
            Vector3 dir = player.transform.position - transform.position;
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), homingSpeed * Time.deltaTime);
            transform.forward = Vector3.RotateTowards(transform.forward, dir, homingSpeed * Time.deltaTime, 0);
        }
        else
        {
            if(Mathf.Abs(player.transform.position.z - transform.position.z) > killAt)
            {
                KillMe();
            }
        }
        
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        aimTime -= Time.deltaTime;
        Debug.DrawRay(transform.position, transform.forward * 5, Color.green);
        //stop persuit
        if (aimTime < 0)
        {
            if (Vector3.Dot(player.transform.forward, transform.forward) > 0)
            {
                homing = false;
            }
        }
    }
    void Patrol()
    {
        if (patrolPath.Length > 1)
        {
            Vector2 target = patrolPath[p];
            Vector2 newPos = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

            if (newPos == target)
            {
                p++;
                if (p >= patrolPath.Length)
                {
                    print("Here");
                    p = 0;
                }
            }
            transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
        }
    }
    void ForwardMovement()
    {
        if (moveForwardAt < 0)
        {
            if (dropFrorwardAt > 0)
            {
                transform.parent = railAnchor.transform;
            }
            else
            {
                if (homing)
                {
                    chase = true;
                }
                transform.parent = null;
            }
        }
        moveForwardAt -= Time.deltaTime;
        dropFrorwardAt -= Time.deltaTime;
    }

}