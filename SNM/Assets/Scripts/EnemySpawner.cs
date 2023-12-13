using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float activationDistance = 10f;
    public GameObject enemyPrefab;


    void Update()
    {
            SpawnEnemy();
            Destroy(gameObject); // Optional: Deactivate the spawner after use
    }

    void SpawnEnemy()
    {
        int randInt;
        randInt = Random.Range(1, 4);
        while(randInt > 0)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
    }
}

