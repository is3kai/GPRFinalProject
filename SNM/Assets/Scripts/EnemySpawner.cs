using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    private GameObject newEnemy;

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
        newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        SpriteRenderer spriteRenderer = newEnemy.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            // Set the sprite (ensure that your prefab has a valid sprite assigned)
            spriteRenderer.sprite = enemyPrefab.GetComponent<SpriteRenderer>().sprite;

            // Reset Sorting Layer and Order in Layer
            spriteRenderer.sortingLayerName = "Default";
            spriteRenderer.sortingOrder = 0;

            // Ensure that the sprite renderer is enabled
            spriteRenderer.enabled = true;
        }

        // Access the enemy script and set up references if needed
        EnemyController enemyController = newEnemy.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            
        }
    }
}
