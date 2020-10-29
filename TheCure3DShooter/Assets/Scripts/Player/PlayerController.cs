using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float directionalSpeed;
    [SerializeField]
    private float forwardSpeed;
 
    Rigidbody rb;
    CapsuleCollider playerCollider;

    public Transform targetingReticule;

    RaycastHit hit;

    [Header("Stats")]
    public GameObject healthBarMeter;
    public int maxHealth;
    [SerializeField]
    int currentHealth;

    public GameObject deathEffect;
    MeshRenderer meshRenderer;
    public float invulnerabilityTime;
    float invulnerabilityCountdown;
    bool invulnerabilityActive = false;

    [Header("Weapon")]
    public int clipSize = 1;
    public GameObject laserShotPrefab;
    GameObject[] laserShots;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        meshRenderer = GetComponent<MeshRenderer>();


        laserShots = new GameObject[clipSize];

        for (int i = 0; i < laserShots.Length; i++)
        {
            laserShots[i] = Instantiate(laserShotPrefab);
        }

        currentHealth = maxHealth;
        
        SetPlayerUi();
        
    }


    void Update()
    {
        transform.LookAt(targetingReticule.position);

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetButton("Fire3"))
        {
            //boost funktion
        }

    }

    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + forwardSpeed * Time.deltaTime);
    }


    void FixedUpdate()
    {
        Movement();

        if (invulnerabilityActive)
        {
            meshRenderer.enabled = !meshRenderer.enabled;
            invulnerabilityCountdown -= Time.deltaTime;
            if (invulnerabilityCountdown <= 0)
            {
                playerCollider.enabled = true;
                invulnerabilityActive = false;
                meshRenderer.enabled = true;
            }
        }
    }
    private void SetPlayerUi()
    {
        healthBarMeter.GetComponent<RectTransform>().localScale = new Vector3(currentHealth, 1, 0);
    }
    private void Shoot()
    {
        
        Debug.DrawRay(transform.position, transform.forward * 20f, Color.red, 10f);

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            Debug.Log("hit a " + hit.collider.gameObject.name);
        }

        foreach (GameObject laserShot in laserShots)
        {
            if (!laserShot.activeSelf)
            {
                laserShot.SetActive(true);
                break;
            }
        }

    }

    void Movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical);

        if (movement.sqrMagnitude > 1)
        {
            movement.Normalize();
        }

        //rb.MovePosition(new Vector3(rb.position.x, rb.position.y, playArea.transform.position.z));
        //float playAreaDiff = playArea.transform.position.z - transform.position.z;
        //Vector3 thrust = new Vector3(0, 0, playAreaDiff);
        //transform.position += thrust;

        rb.AddForce(movement * directionalSpeed);
        
        //rb.AddForce(thrust);
    }

    public void onHit(int damage)
    {
        currentHealth -= damage;
        Instantiate (deathEffect, transform.position, Quaternion.identity);
        SetPlayerUi();

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            //gameOver
        }

        invulnerabilityActive = true;
        playerCollider.enabled = false;
        invulnerabilityCountdown = invulnerabilityTime;

    }


}
