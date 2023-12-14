using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaucerController : MonoBehaviour
{
    private Rigidbody saucerRb;

    //movement varables
    public float moveSpeed;

    //gamemanager
    private GameManager gameManager;

    //points asteroid is worth
    public int pointValue;

    public GameObject shotPrefab;
    public GameObject shotSpawn;
    public float timeBetweenShots;

    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        saucerRb = GetComponent<Rigidbody>();

        
        StartCoroutine(Shoot());

        //chooses a direction to move in
        RandomMove();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if player's shot or powerup shield collides with asteroid asteroid is destroyed
        if (other.gameObject.CompareTag("PlayerShot") || other.gameObject.CompareTag("Shield"))
        {
            //particle system
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);

            //gives points
            gameManager.AddScore(pointValue);

            //destroys saucer
            Destroy(gameObject);

            //if hit with player's shot, destroys projectile
            if (other.gameObject.CompareTag("PlayerShot"))
            {
                Destroy(other.gameObject);
            }
        }
    }
    public void RandomMove()
    {
        //chooses a random direction to move in
        Vector3 lookDirection = new Vector3(Random.Range(-15, 15), Random.Range(-15, 15), -1.5f);

        //sets asteroid moving in that direction
        saucerRb.AddRelativeForce(lookDirection * moveSpeed);
    }

    IEnumerator Shoot()
    {
        shotSpawn.transform.Rotate(0, 0, Random.Range(-360, 360));
        yield return new WaitForSeconds(timeBetweenShots);
        Instantiate(shotPrefab, shotSpawn.transform.position, shotSpawn.transform.rotation);
        StartCoroutine(Shoot());
    }

}
