using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //variables
    public float boostForce;
    public float turnSpeed;
    private float horizontalInput;
    private Rigidbody playerRb;
    public GameObject projectilePrefab;
    public GameObject projectileSpawn;
    private GameManager gameManager;
    public GameObject boostIndicator;
    public GameObject powerupIndicator;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        boostIndicator.gameObject.SetActive(false);
        powerupIndicator.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);
        if (Input.GetButtonDown("Boost"))
        {
            StartCoroutine (Boost());
        }
        if (Input.GetButtonDown("Shoot"))
        {
            Instantiate(projectilePrefab, projectileSpawn.transform.position, projectileSpawn.transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            Destroy(gameObject);
            gameManager.GameOver();
        }

        if (other.gameObject.CompareTag("PowerUp"))
        {
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
        }
    }

    IEnumerator Boost()
    {
        playerRb.AddRelativeForce(Vector3.right * boostForce, ForceMode.Impulse);
        boostIndicator.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        boostIndicator.gameObject.SetActive(false);
    }
}
