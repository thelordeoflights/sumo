using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyprefabs
    ;
    public GameObject[] Powerupprefabs;

    private float SpawnRange = 9.0f;
    public int enemyCount;
    public int waveNumber = 1;

    void Start()
    {
        int randomPowerup = Random.Range(0, Powerupprefabs.Length);
        Instantiate(Powerupprefabs[randomPowerup], GeneratespawnPosition(), Powerupprefabs[randomPowerup].transform.rotation);
        SpawnEnemyWave(waveNumber);

    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        int enemyIndex = Random.Range(0, enemyprefabs
        .Length);
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyprefabs
            [enemyIndex], GeneratespawnPosition(), enemyprefabs
            [enemyIndex].transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            int randomPowerup = Random.Range(0, Powerupprefabs.Length);
            Instantiate(Powerupprefabs[randomPowerup], GeneratespawnPosition(), Powerupprefabs[randomPowerup].transform.rotation);
        }
    }
    private Vector3 GeneratespawnPosition()
    {
        float spawnPosX = Random.Range(-SpawnRange, SpawnRange);
        float spawnPosZ = Random.Range(-SpawnRange, SpawnRange);

        Vector3 RandomPosition = new Vector3(spawnPosX, 0, spawnPosZ);
        return RandomPosition;
    }
}
