using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Vector3 orgPos;
    public bool randomSpin = true;
    public float RandomSpinTimeOne = 0.5f;
    public float RandomSpinTimeTwo = 1.0f;
    public float RandomSpinTimeTre = 1.25f;

    public Vector3[] patrolPath;
    public float moveSpeed;
    private int p = 0;

    // Start is called before the first frame update
    void Start()
    {
        orgPos = transform.position;
        RandomSpinTimeOne = Random.Range(2, 3);
        RandomSpinTimeTwo = RandomSpinTimeOne * Random.Range(1, 2);
        RandomSpinTimeTre = RandomSpinTimeTwo * Random.Range(-1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(patrolPath.Length > 1)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, orgPos + patrolPath[p], moveSpeed * Time.deltaTime);
            if(transform.position == orgPos + patrolPath[p])
            {
                p++;
                if(p >= patrolPath.Length)
                {
                    print("Here");
                    p = 0;
                }
            }
        }


        if (randomSpin)
        {
            transform.RotateAround(transform.up, RandomSpinTimeOne * Time.deltaTime);
            transform.RotateAround(transform.right, RandomSpinTimeTwo * Time.deltaTime);
            transform.RotateAround(transform.forward, RandomSpinTimeTre * Time.deltaTime);
        }
    }

    
}
