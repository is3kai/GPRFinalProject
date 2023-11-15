using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    private GameObject currentEnemy;

    public static EnemySpawner Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        // Spawn a new enemy
        currentEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        // Access the enemy script and set up references if needed
        EnemyController enemyController = currentEnemy.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            
        }
    }
}
