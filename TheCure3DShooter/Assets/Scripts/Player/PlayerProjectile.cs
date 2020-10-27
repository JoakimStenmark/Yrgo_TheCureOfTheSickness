using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float maxTimeAlive;
    private float timer;

    [Header("Settings")]
    public LayerMask rayMask;
    public bool laser = true;
    public bool homingMissile = false;

    public float speed = 10;

    [Header("FX")]
    public GameObject onHitFX;

    private void OnEnable()
    {
         
        
        //Transform playerTransform = GetComponentInParent<Transform>();
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        transform.SetPositionAndRotation(playerTransform.position, playerTransform.rotation);
        timer = maxTimeAlive;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            gameObject.SetActive(false);
        }
    }
    void FixedUpdate()
    {
        if (laser)
            LaserBeam();


    }

    private void LaserBeam()
    {
        Debug.DrawRay(transform.position, transform.forward * speed * Time.fixedDeltaTime, Color.red, 1);

        RaycastHit rayHit;
        if (Physics.Raycast(transform.position, transform.forward, out rayHit, speed * Time.fixedDeltaTime, rayMask))
        {
            if (onHitFX != null)
            {
                Instantiate(onHitFX, transform.position, Quaternion.identity);
            }
            
            gameObject.SetActive(false);
        }
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }
}
