using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health of the enemy
    public int currentHealth;   // Current health of the enemy

    // Optional: You can add an event or delegate to be triggered when the enemy is defeated
    public delegate void EnemyDefeated();
    public static event EnemyDefeated OnEnemyDefeated;

    void Start()
    {
        currentHealth = maxHealth; // Set current health to max health when the enemy is spawned
    }

    // Method to handle damage taken by the enemy
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce current health by the amount of damage taken

        // Check if the enemy has been defeated
        if (currentHealth <= 0)
        {
            Die(); // Call the Die() method if the enemy has no health remaining
        }
    }

    // Method to handle the enemy's death
    void Die()
    {
        // Optional: Trigger an event when the enemy is defeated
        if (OnEnemyDefeated != null)
        {
            OnEnemyDefeated();
        }

        // Destroy the enemy GameObject
        Destroy(gameObject);
    }
}