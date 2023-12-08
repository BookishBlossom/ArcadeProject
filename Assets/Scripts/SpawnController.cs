using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public GameObject powerupPrefab;
    public int enemyCount;
    public float spawnRange;
    public float safeRange;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(4);
        SpawnPowerup();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX=0;
        float spawnPosY = 0;
        while (spawnPosX < safeRange && spawnPosX > -safeRange)
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

}
