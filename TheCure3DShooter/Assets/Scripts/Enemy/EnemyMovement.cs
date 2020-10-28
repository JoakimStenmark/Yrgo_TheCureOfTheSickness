using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Vector3 orgPos;
    private Rigidbody rb;
    public bool randomSpin = true;
    public float RandomSpinTimeOne = 0.5f;
    public float RandomSpinTimeTwo = 1.0f;
    public float RandomSpinTimeTre = 1.25f;

    [Header("Behavior")]
    public float moveForwardAt = 2;
    public float dropFrorwardAt = 5;
    public float forwardSpeed = 5;
    public float moveSpeed = 5;
    public int enemySpawNumber;
    private Vector3[] patrolPath;
    private int p = 0;

    public Vector3 forwardMoveDir = Vector3.forward;
    public Vector3 forwardPos = Vector3.forward;

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
        CircularPath(5,24);
        p = enemySpawNumber;

        transform.position = new Vector3(patrolPath[p].x, patrolPath[p].y, transform.position.z);

        RandomSpinTimeOne = Random.Range(2, 3);
        RandomSpinTimeTwo = RandomSpinTimeOne * Random.Range(1, 2);
        RandomSpinTimeTre = RandomSpinTimeTwo * Random.Range(-1, 1);
    }

    // Update is called once per frame
    void Update()
    {

        ForwardMovement();

        if (patrolPath.Length > 1)
        {

            Vector2 ePos = transform.position;
            Vector2 target = patrolPath[p];
            Vector2 newPos = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

            if(newPos == target)
            {
                p++;
                if(p >= patrolPath.Length)
                {
                    print("Here");
                    p = 0;
                }
            }
            transform.position = new Vector3(newPos.x, newPos.y, forwardPos.z);
        }

        if (randomSpin)
        {
            transform.Rotate(transform.up, RandomSpinTimeOne * Time.deltaTime);
            transform.Rotate(transform.right, RandomSpinTimeTwo * Time.deltaTime);
            transform.Rotate(transform.forward, RandomSpinTimeTre * Time.deltaTime);
        }
    }

    void ForwardMovement()
    {
        if(moveForwardAt < 0)
        {
            if(dropFrorwardAt > 0) {
                forwardPos += Vector3.forward * Time.deltaTime;
                forwardMoveDir = Vector3.forward * Time.deltaTime;
            }
        }
        moveForwardAt -= Time.deltaTime;
        dropFrorwardAt -= Time.deltaTime;
    }

    void CircularPath(float radius, int segment)
    {
        Vector3 point = Vector3.up * radius;
        patrolPath = new Vector3[segment];

        float angle = 360 / segment;

        for (int i = 0; i < segment; i++)
        {
            Quaternion rotstep = Quaternion.AngleAxis(angle * i, Vector3.forward);
            patrolPath[i] = rotstep * point;
            Debug.DrawRay(patrolPath[i], Vector3.one * 0.25f, Color.yellow, 10);
        }
    }
}
