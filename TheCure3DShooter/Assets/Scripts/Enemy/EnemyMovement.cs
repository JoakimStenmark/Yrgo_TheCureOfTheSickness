//Robert S
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject railAnchor;
    private Vector2[] patrolPath;
    private int p = 0;

    [Header("Behavior")]
    public float moveForwardAt = 2;
    public float dropFrorwardAt = 5;
    public float forwardSpeed = 5;
    public float moveSpeed = 5;
    
    public void SpawnInit(int spawNumber, Vector2[] path, GameObject ra)
    {
        p = spawNumber;
        patrolPath = path;
        railAnchor = ra;

        transform.position = new Vector3(patrolPath[p].x, patrolPath[p].y, transform.position.z);
    }
    private void OnCollisionEnter(Collision collision)
    {
        KillMe();
    }

    public void KillMe()
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ForwardMovement();
        Patrol();
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
        if(moveForwardAt < 0)
        {
            if(dropFrorwardAt > 0) {
                transform.parent = railAnchor.transform;
            }
            else
            {
                transform.parent = null;
            }
        }
        moveForwardAt -= Time.deltaTime;
        dropFrorwardAt -= Time.deltaTime;
    }
}
