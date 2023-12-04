using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Transform player; // Reference to the player's position
    private NavMeshAgent agent;

    public int maxHealth = 3;
    private int currentHealth;
    public int damageAmount = 10;
    private float damageCooldown = 3f;
    private float nextDamageTime = 0f;

    void Start()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        if (player == null)
        {
            Debug.LogError("Player not found! Make sure to tag your player object.");
        }

        currentHealth = maxHealth;
    }

    void Update()
    {
        // Set the destination to the player's position
        if (player != null)
        {
            agent.SetDestination(player.position);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time >= nextDamageTime)
            {
                // Deal damage to the player
                PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount);
                }

                // Set the next allowed damage time
                nextDamageTime = Time.time + damageCooldown;
            }
        }
    }

    void OnMouseDown()
    {
        // Called when the enemy is clicked
        TakeDamage(1); // You can adjust the damage value as needed
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Trigger the enemy spawner to spawn a new enemy
        EnemySpawner.Instance.SpawnEnemy();

        // Add any death behavior (e.g., play death animation, spawn particles)
        Destroy(gameObject);
    }
}
