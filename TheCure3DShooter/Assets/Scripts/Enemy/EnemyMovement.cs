//Robert S
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Propetys")]
    public int hp = 1;
    public int dmg = 1;
    private GameObject player;
    private GameObject railAnchor;
    private Vector2[] patrolPath;
    private int p = 0;

    [Header("FX")]
    public GameObject fxOnDeath;

    [Header("Behavior")]
    public float moveForwardAt = 2;
    public float dropFrorwardAt = 5;
    public float patrolSpeed = 3;
    private float moveSpeed = 10;
    public bool homing = true;
    private bool chase;
    public float chaseSpeed = 10;

    public float aimTime = 2;
    private float homingAngularSpeed = 2;

    public Vector3 homingTargetOffsett = Vector3.forward * 5;
    public float killAt = 20;

    private Transform visual;

    public void SpawnInit(int spawNumber, Vector2[] path, GameObject ranchor, GameObject pl)
    {
        visual = transform.GetChild(0);
        visual.parent = null;

        moveSpeed = patrolSpeed;
        p = spawNumber;
        patrolPath = path;
        railAnchor = ranchor;
        player = pl;

        transform.position = new Vector3(patrolPath[p].x, patrolPath[p].y, transform.position.z);

        transform.forward = player.transform.position - transform.position;
    }
    private void OnCollisionEnter(Collision collision)
    {
        PlayerController hp = collision.collider.GetComponent<PlayerController>();
        
        if (hp != null)
        {
            hp.onHit(1);
        }
        KillMe();
    }

    public void OnHit(int dmg)
    {
        hp--;
        if(hp <= 0)
            KillMe();
    }
    private void KillMe()
    {
        Instantiate(fxOnDeath, transform.position, transform.rotation);
        Destroy(visual.gameObject);
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

        VisualObject();
    }

    void VisualObject()
    {
        visual.position = transform.position;
    }

    void HomingOnPlayer()
    {
        moveSpeed = Mathf.MoveTowards(moveSpeed, chaseSpeed, 4 * Time.deltaTime);

        if (homing)
        {
            Vector3 dir = player.transform.position + homingTargetOffsett - transform.position;
            transform.forward = Vector3.RotateTowards(transform.forward, dir, homingAngularSpeed * Time.deltaTime, 0);
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
            Vector3 newPosition = new Vector3(newPos.x, newPos.y, transform.position.z);
            transform.forward = newPosition - transform.position;
            transform.position = newPosition;// new Vector3(newPos.x, newPos.y, transform.position.z);
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