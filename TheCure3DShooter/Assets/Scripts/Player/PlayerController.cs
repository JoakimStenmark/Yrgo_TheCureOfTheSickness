//Joakim
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
    //public AudioClip damageSound;


    public GameObject deathEffect;
    MeshRenderer meshRenderer;
    public float invulnerabilityTime;
    float invulnerabilityCountdown;
    bool invulnerabilityActive = false;

    [Header("Weapon")]
    public int clipSize = 1;
    public GameObject laserShotPrefab;
    GameObject[] laserShots;
    AudioSource audioSource;
    
    public AudioClip[] weaponSounds;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();
       
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

        if (Input.GetButton("Fire3") && GameManager.instance.debugActive)
        {
            //boost funktion
            onHit(4);

        }

        if (invulnerabilityActive)
        {
            
            invulnerabilityCountdown -= Time.deltaTime;
            if (invulnerabilityCountdown <= 0)
            {
                playerCollider.enabled = true;
                invulnerabilityActive = false;
                meshRenderer.enabled = true;
            }
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

            if (invulnerabilityCountdown <= 0)
            {

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
            //Debug.Log("hit a " + hit.collider.gameObject.name);
        }
        int clipNumber = UnityEngine.Random.Range(0, weaponSounds.Length);
        audioSource.clip = weaponSounds[clipNumber];
        audioSource.pitch = UnityEngine.Random.Range(0.8f, 1.1f);

        foreach (GameObject laserShot in laserShots)
        {
            if (!laserShot.activeSelf)
            {
                laserShot.SetActive(true);
                audioSource.Play();
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

        rb.AddForce(movement * directionalSpeed);
      
    }

    public void onHit(int damage)
    {
        if (damage > 0)
        {
            currentHealth -= damage;
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            SetPlayerUi();
            
            //audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            //audioSource.clip = damageSound;
            //audioSource.Play();

            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);
                GameManager.instance.ChangeGameState(GameManager.GameState.GameOver);
            }

            invulnerabilityActive = true;
            playerCollider.enabled = false;
            invulnerabilityCountdown = invulnerabilityTime;
        }
    }


}
