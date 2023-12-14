using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    //prefabs of gameobjects to spawn
    public GameObject asteroidPrefab;
    public GameObject powerupPrefab;
    public GameObject saucerPrefab;


    public float timeBetweenSaucers;

    //waves
    private int asteroidCount;
    private int waveNumber = 4;

    //range inside the playing field
    public float spawnRange;

    // range of space around player spawn safe from asteroids spawning
    public float safeRange;



    // Start is called before the first frame update
    void Start()
    {
        //spawns 4 asteroids and a powerup at the start
        SpawnEnemyWave(waveNumber);
        SpawnPowerup();
        StartCoroutine(WaitForSaucerTime());
    }

    // Update is called once per frame
    private void Update()
    {
        asteroidCount = FindObjectsOfType<AsteroidController>().Length;
        if (asteroidCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
        Debug.Log("Asteroid Count: " + asteroidCount);
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(asteroidPrefab, GenerateSpawnPosition(), asteroidPrefab.transform.rotation);
        }
    }

    void SpawnPowerup()
    {
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }

    //generates random spawn positions for tthe asteroids that don't overlap with the player's spawn
    private Vector3 GenerateSpawnPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        float spawnPosX=0;
        float spawnPosY = 0;

        while ((spawnPosX < safeRange && spawnPosX > -safeRange))
        {
            spawnPosX = Random.Range(-spawnRange, spawnRange);
        }

        while (spawnPosY < safeRange && spawnPosY > -safeRange)
        {
            spawnPosY = Random.Range(-spawnRange, spawnRange);
        }

        Vector3 randomPos = new Vector3(spawnPosX, spawnPosY, -2);
        return randomPos;
    }

    IEnumerator WaitForSaucerTime()
    {
        yield return new WaitForSeconds(30);
        StartCoroutine(SpawnSaucer());
    }
    IEnumerator SpawnSaucer()
    {
        Vector3 saucerSpawnPos = new Vector3(-3.5f, Random.Range(-spawnRange, spawnRange), 0);
        yield return new WaitForSeconds(timeBetweenSaucers);
        Instantiate(saucerPrefab, GenerateSpawnPosition(), saucerPrefab.transform.rotation);
        StartCoroutine(SpawnSaucer());
    }
}
