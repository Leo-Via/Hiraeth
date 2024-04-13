using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum health of the player
    public float currentHealth;   // Current health of the player
    public Image healthBar;        // Health bar of the player
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

    //Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(currentHealth / maxHealth, 0, 1); //Changes the fill of the health bar when health changes. Won't exceed math health
    }

    // Method to handle the player's death
    void Die()
    {
        // Perform any actions related to the player's death
        Debug.Log("Player has been defeated!");
        // For example, you could show a game over screen, respawn the player, etc.
    }
}