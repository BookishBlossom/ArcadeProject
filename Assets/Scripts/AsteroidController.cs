using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsteroidController : MonoBehaviour
{
    //movement varables
    public float moveSpeed;
    public float rotateSpeed;

    //gamemanager
    private GameManager gameManager;

    //points asteroid is worth
    public int pointValue;

    //asteroid rigidbody
    private Rigidbody asteroidRb;

    //smaller asteroid prefab
    public GameObject miniAsteroid;

    //audio variables
    private AudioSource asteroidAudio;
    public AudioClip explodeSound;


    // Start is called before the first frame update
    void Start()
    {
        //gets various components
        asteroidAudio = GetComponent<AudioSource>();
        asteroidRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        //spawns with a randomized rotation
        transform.Rotate(0, Random.Range(-360, 360), 0);
        RandomMove();
    }

    // Update is called once per frame
    void Update()
    {
        //spins asteroid
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        //if player's shot or powerup shield collides with asteroid asteroid is destroyed
        if (other.gameObject.CompareTag("PlayerShot") || other.gameObject.CompareTag("Shield"))
        {
            //gives points
            gameManager.AddScore(pointValue);

            //triggers split in two
            Shatter();

            //destroys game object
            Destroy(gameObject);

            //if hit with player's shot, destroys projectile
            if (other.gameObject.CompareTag("PlayerShot"))
            {
                Destroy(other.gameObject);
            }
        }
    }

    //asteroid splits into 2 smaller pieces
    public void Shatter()
    {
        // gets asteroid's x and y position
        float posX = transform.position.x;
        float posY = transform.position.y;

        //spawns both asteroids in seperate positions
        Instantiate(miniAsteroid, new Vector3(posX + 1f, posY, -1.5f), transform.rotation);
        Instantiate(miniAsteroid, new Vector3(posX - 1f, posY, -1.5f), transform.rotation);
    }

    
    public void RandomMove()
    {
        //chooses a random direction to move in
        Vector3 lookDirection = new Vector3(Random.Range(-15, 15), Random.Range(-15, 15), -1.5f);

        //sets asteroid moving in that direction
        asteroidRb.AddRelativeForce(lookDirection * moveSpeed);
    }
}
