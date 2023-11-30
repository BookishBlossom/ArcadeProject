using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    private GameManager gameManager;
    public int pointValue;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerShot"))
        {
            gameManager.UpdateScore(pointValue);
            Shatter();
            Destroy(gameObject);
        }
    }

    public void Shatter()
    {
        for (int i = 0; i < 5; i++)
        {
            // should the smaller asteroids be a different prefab? or is there a better way to do this
            // write something to generate spawn coordinates?
            // wait what if instead, the smaller sasteroids were inside the larger one. and they just get unhidden by
            // Instantiate(miniAsteroid, transform.position, transform.rotation); 
        }
    }
}
