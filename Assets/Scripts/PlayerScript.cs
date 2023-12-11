using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //variables
    //boost variables
    public float boostForce;
    public GameObject boostIndicator;
    private Rigidbody playerRb;

    //rotation variables
    public float turnSpeed;
    private float horizontalInput;

    //shooting variables
    public GameObject projectilePrefab;
    public GameObject projectileSpawn;

    //gamemanager
    private GameManager gameManager;

    //powerup variables
    public GameObject powerupIndicator;
    private bool shielded = false;

    //audio variables
    private AudioSource playerAudio;
    public AudioClip shootSound;
    public AudioClip boostSound;
    public AudioClip powerupSound;
    public AudioClip powerdownSound;
    public AudioClip shieldCrashSound;

    public float maxVelocity;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        boostIndicator.gameObject.SetActive(false);
        powerupIndicator.gameObject.SetActive(false);
        shielded = false;
    }

    // Update is called once per frame
    void Update()
    {
        // rotates the player according to input
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);

        //gets input for boost
        if (Input.GetButtonDown("Boost"))
        {
            StartCoroutine(Boost());
        }

        //gets input for shoot
        if (Input.GetButtonDown("Shoot"))
        {
            playerAudio.PlayOneShot(shootSound, 1.0f);
            Instantiate(projectilePrefab, projectileSpawn.transform.position, projectileSpawn.transform.rotation);
        }
    }

    //player colisions
    private void OnTriggerEnter(Collider other)
    {
        //if player collides with asteroid when not shielded, player loses a life
        if (other.gameObject.CompareTag("Asteroid") && !shielded)
        {
            gameManager.updateLifeCount();
            Destroy(gameObject);
        }
        
        //if player colides with powerup, player gains powerup
        if (other.gameObject.CompareTag("PowerUp"))
        {
            playerAudio.PlayOneShot(powerupSound, 1.0f);
            Destroy(other.gameObject);
            StartCoroutine(Powerup());
        }
    }

    //boost code
    IEnumerator Boost()
    {
        //plays sound effect
        playerAudio.PlayOneShot(boostSound, 0.25f);

        //adds the force
        playerRb.AddRelativeForce(Vector3.right * boostForce, ForceMode.Impulse);

        //displays boost indicator, hides it again.
        boostIndicator.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        boostIndicator.gameObject.SetActive(false);

        //checks if the player is moving to fast
        if(playerRb.velocity.magnitude > maxVelocity)
        {
            //stops the player from moving too fast
            RegulateVelocity();
        }
    }

    public void RegulateVelocity()
    {
        Vector3 playerVelocity = playerRb.velocity;
        playerRb.velocity = playerVelocity.normalized * maxVelocity;
    }

    //powerup code
    IEnumerator Powerup()
    {
        //shielded is true, makes player invulnerable to asteroids
        shielded = true;

        //shows powerup indicator
        powerupIndicator.gameObject.SetActive(true);
        
        //waits a few seconds
        yield return new WaitForSeconds(5);

        //powerup effects disabled
        shielded = false;
        powerupIndicator.gameObject.SetActive(false);

        //sound effect to let the player know the powerup is over
        playerAudio.PlayOneShot(powerdownSound, 1.0f);
    }
}
