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
    public GameManager gameManager;



    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);
        if (Input.GetButtonDown("Boost"))
        {
            playerRb.AddRelativeForce(Vector3.right * boostForce, ForceMode.Impulse);
        }
        if (Input.GetButtonDown("Shoot"))
        {
            Instantiate(projectilePrefab, projectileSpawn.transform.position, projectileSpawn.transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Asteroid"))
        {
            gameManager.GameOver();
        }
    }
}
