using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceMoveEnemy : MonoBehaviour
{
    [Header("MoveVector")]
    public Vector3 targetDir;
    private Rigidbody rb;
    private float speed = 10;

    [Header("Behaviour")]
    [Header("Passive")]
    public float cruseSpeed = 10;
    public Vector3[] patrolDirections;
    public float patrolDirTime = 5;
    private int d = 0;
    private float timeToSwitch = 0;

    [Header("Agressive")]
    public float chaseSpeed = 20;
    public float reactToPlayerDist = 0;
    public GameObject player;
    [Header("Weapon")]
    public GameObject projectileType;
    public float fireRate = 2;
    public bool randomFireAtStart = true;
    private float timeToNextShoot;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        speed = cruseSpeed;

        if (randomFireAtStart)
        {
            timeToNextShoot = Random.Range(0, fireRate);
        }
        else
        {
            timeToNextShoot = fireRate;
        }
    }

    private void Update()
    {
        //test
        if (Input.GetButtonDown("Fire1"))
        {
            rb.AddForce(Vector3.forward * 100);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(patrolDirections.Length > 0)
            targetDir = MoveInDirection();

        if(reactToPlayerDist > 0)
        {
            targetDir = ChasePlayer(targetDir);
        }

        float speedMul = BrakeSpeedMul(targetDir, rb.velocity);
        if (speedMul < 0)
            print("Smaller then zero" + speedMul);
        rb.AddForce(targetDir * speed * speedMul * Time.deltaTime);

        if (projectileType != null)
            Fire();

        //Debug
        Debug.DrawRay(transform.position, targetDir * 10 * speedMul, Color.green);
    }

    private void Fire()
    {
        timeToNextShoot -= Time.fixedDeltaTime;
        if(timeToNextShoot < 0)
        {
            Vector3 fireDir = player.transform.position - transform.position;
            Instantiate(projectileType, transform.position + fireDir.normalized, Quaternion.LookRotation(fireDir));
            timeToNextShoot = fireRate;
        }
    }
    private Vector3 MoveInDirection()
    {
        timeToSwitch += Time.fixedDeltaTime;
        if (timeToSwitch > patrolDirTime)
        {
            d++;
            if (d >= patrolDirections.Length)
            {
                d = 0;
            }
            timeToSwitch = 0;
        }
        return patrolDirections[d];
    }

    private Vector3 ChasePlayer(Vector3 currentTarget)
    {
        Vector3 dirToPlayer = player.transform.position - transform.position;
        float distToPlayer = dirToPlayer.sqrMagnitude;
        if (distToPlayer < reactToPlayerDist * reactToPlayerDist)
        {
            speed = chaseSpeed;
            return dirToPlayer;
        }
        return currentTarget;
    }

    private float BrakeSpeedMul(Vector3 wantedMoveDir, Vector3 actualMoveDir)
    {
        float dot = Vector3.Dot(wantedMoveDir.normalized, actualMoveDir.normalized);
        dot += 1;
        dot = 3 - dot;
        dot *= 2;
        return dot;
    }

    /*
    public Vector3[] patrolPath;
    public float switchNodeSqrDist = 2;
    private int p = 0;

         if (patrolPath.Length > 1)
        {
            PathControl(dir, velocity);
        }

    void PathControl(Vector3 dir, Vector3 velocity)
    {
        target = patrolPath[p];

        //If within distance
        if (dir.sqrMagnitude < switchNodeSqrDist)
        {
            //If wanted direction = wanted speed
            if (Vector3.Dot(dir, velocity) < 0)
            {
                p++;
                if (p >= patrolPath.Length)
                    p = 0;
            }
        }
    }
    */
}
