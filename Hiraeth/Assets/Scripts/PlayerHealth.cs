using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum health of the player
    private float currentHealth;   // Current health of the player

    void Start()
    {
        currentHealth = maxHealth; // Set current health to max health when the player is spawned
    }

    // Method to handle damage taken by the player
    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // Reduce current health by the amount of damage taken

        // Check if the player has been defeated
        if (currentHealth <= 0)
        {
            Die(); // Call the Die() method if the player has no health remaining
        }
    }

    // Method to handle the player's death
    void Die()
    {
        // Perform any actions related to the player's death
        Debug.Log("Player has been defeated!");
        // For example, you could show a game over screen, respawn the player, etc.
    }
}