//Robert S
//When time use Rigidbody move position
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Propetys")]
    public float killRange = 100;
    public int hp = 1;
    public int dmg = 1;
    private Rigidbody rb;
    private GameObject player;
    private GameObject railAnchor;
    private Vector2[] patrolPath;
    private int p = 0;

    [Header("FX")]
    public GameObject fxOnDeath;

    [Header("Behavior")]
    public float plSyncAtSec = 2;
    public float dropPlSyncAtSec = 5;
    public float patrolSpeed = 3;
    private float moveSpeed = 10;
    public bool homing = true;
    private bool chase;
    public float chaseSpeed = 10;
    public float chaseAccMul = 10;

    public float aimTimeOut = 2;
    private float homingAngularSpeed = 2;

    public Vector3 homingTargetOffsett = Vector3.forward * 5;
    public float killAt = 20;

    private Transform visualObject;

    public void SpawnInit(int spawNumber, Vector2[] path, GameObject ranchor, GameObject pl)
    {
        rb = GetComponent<Rigidbody>();
        visualObject = transform.GetChild(0);
        visualObject.parent = null;

        moveSpeed = patrolSpeed;
        p = spawNumber;
        patrolPath = path;
        railAnchor = ranchor;
        player = pl;

        transform.position = new Vector3(patrolPath[p].x, patrolPath[p].y, transform.position.z);
        transform.forward = player.transform.position - transform.position;
    }
   


    // Update is called once per frame
    private void Update()
    {
        Visuals();
    }
    void FixedUpdate()
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

        //Visuals();
        KillOnRange();
    }


    void HomingOnPlayer()
    {
        moveSpeed = Mathf.MoveTowards(moveSpeed, chaseSpeed, chaseAccMul * Time.deltaTime);

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
        //rb.MovePosition(transform.forward * moveSpeed);

        aimTimeOut -= Time.deltaTime;
        Debug.DrawRay(transform.position, transform.forward * 5, Color.green);
        //stop persuit
        if (aimTimeOut < 0)
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
            transform.position = newPosition;
            //rb.MovePosition(transform.forward * moveSpeed * Time.fixedDeltaTime);
        }
    }
    void ForwardMovement()
    {
        if (plSyncAtSec < 0)
        {
            if (dropPlSyncAtSec > 0)
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
        plSyncAtSec -= Time.deltaTime;
        dropPlSyncAtSec -= Time.deltaTime;
    }

    void Visuals()
    {
        visualObject.position = transform.position;
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
        if (hp <= 0)
            KillMe();
    }
    private void KillMe()
    {
        Instantiate(fxOnDeath, transform.position, transform.rotation);
        Destroy(visualObject.gameObject);
        Destroy(gameObject);
    }

    void KillOnRange()
    {
        if (transform.position.z - player.transform.position.z < -killRange)
            SilentKill();
    }

    private void SilentKill()
    {
        Destroy(visualObject.gameObject);
        Destroy(gameObject);
    }
}