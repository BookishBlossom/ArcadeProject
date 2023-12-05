using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    private GameManager gameManager;
    public int pointValue;
    private Rigidbody asteroidRb;
    public GameObject miniAsteroid;

    // Start is called before the first frame update
    void Start()
    {
        asteroidRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        RandomMove();
        asteroidRb.AddRelativeForce(Vector3.right * moveSpeed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerShot"))
        {
            gameManager.UpdateScore(pointValue);
            Shatter();
            Destroy(gameObject);
        }
    }

    public void Shatter()
    {
        float posX = transform.position.x;
        float posY = transform.position.y;
        Instantiate(miniAsteroid, new Vector3(posX + 1f, posY, -1.5f), transform.rotation);
        Instantiate(miniAsteroid, new Vector3(posX - 1f, posY, -1.5f), transform.rotation);
    }

    public void RandomMove()
    {
        // Vector3 lookDirection = ( - transform.position).normalized;
        // rb.AddForce(lookDirection * moveSpeed);
    }
}
