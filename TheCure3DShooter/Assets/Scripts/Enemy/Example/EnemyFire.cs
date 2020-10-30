//Robert S
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public bool homing = true;
    public Transform target;
    public float fireRate = 2;
    private float nextShoot;

    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        nextShoot = Random.Range(0, fireRate);
    }

    // Update is called once per frame
    void Update()
    {
        nextShoot -= Time.deltaTime;
        if(nextShoot < 0)
        {
            Vector3 dir = target.position + Vector3.forward * 5 - transform.position;
            Instantiate(projectile, transform.position, Quaternion.LookRotation(dir));
            nextShoot = fireRate;
        }
    }
}
