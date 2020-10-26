using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public bool randomSpin = false;
    public float RandomSpinTimeOne = 0.5f;
    public float RandomSpinTimeTwo = 1.0f;
    public float RandomSpinTimeTre = 1.25f;
    private Vector3 spinAxisOne = Vector3.up;
    private Vector3 spinAxisTwo = Vector3.forward;
    private Vector3 spinAxisTre = Vector3.left;

    // Start is called before the first frame update
    void Start()
    {
        RandomSpinTimeOne = Random.Range(-1, 1);
        RandomSpinTimeTwo = RandomSpinTimeOne * Random.Range(-1, 1);
        RandomSpinTimeTre = RandomSpinTimeTwo * Random.Range(-1, 1);

        spinAxisOne = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
}

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.up, RandomSpinTimeOne * Time.deltaTime);
        transform.RotateAround(transform.right, RandomSpinTimeTwo * Time.deltaTime);
        transform.RotateAround(transform.forward, RandomSpinTimeTre * Time.deltaTime);
    }
}
