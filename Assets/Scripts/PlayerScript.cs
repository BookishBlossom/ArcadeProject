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
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);
        if (Input.GetButtonDown("Boost"))
        {
            StartCoroutine(Boost());
        }
        if (Input.GetButtonDown("Shoot"))
        {
            playerAudio.PlayOneShot(shootSound, 1.0f);
            Instantiate(projectilePrefab, projectileSpawn.transform.position, projectileSpawn.transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Asteroid") && !shielded)
        {
            Destroy(gameObject);
            gameManager.GameOver();
        }

        if (other.gameObject.CompareTag("PowerUp"))
        {
            playerAudio.PlayOneShot(powerupSound, 1.0f);
            Destroy(other.gameObject);
            StartCoroutine(Powerup());
        }
    }

    IEnumerator Boost()
    {
        playerAudio.PlayOneShot(boostSound, 0.25f);
        playerRb.AddRelativeForce(Vector3.right * boostForce, ForceMode.Impulse);
        boostIndicator.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        boostIndicator.gameObject.SetActive(false);
    }

    IEnumerator Powerup()
    {
        shielded = true;
        powerupIndicator.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        shielded = false;
        powerupIndicator.gameObject.SetActive(false);
        playerAudio.PlayOneShot(powerdownSound, 1.0f);
    }
}
