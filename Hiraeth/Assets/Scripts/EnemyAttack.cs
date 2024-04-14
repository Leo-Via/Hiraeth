using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 2f; // Range within which the enemy can attack
    public float attackDamage = 10f; // Damage inflicted by the enemy's attack
    public LayerMask playerLayer; // Layer containing the player GameObject
    
    AudioManager audioManager;
    private void Awake()
    {
        GameObject audioObject = GameObject.FindGameObjectWithTag("Audio");
        if (audioObject != null)
        {
            audioManager = audioObject.GetComponent<AudioManager>();
            if (audioManager == null)
            {
                Debug.LogError("AudioManager component not found on object with tag 'Audio'");
            }
        }
        else
        {
            Debug.LogError("No GameObject with tag 'Audio' found in the scene");
        }
    }



    private bool isAttacking = false; // Flag to track if the enemy is currently attacking

    void Update()
    {
        // Check for player within attack range
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);

        foreach (Collider2D player in hitPlayers)
        {
            // Attack the player
            AttackPlayer(player.gameObject);
        }
    }

    public void AttackPlayer(GameObject player)
    {
        // Ensure the enemy is not already attacking
        if (!isAttacking)
        {
            // Deal damage to the player
            // Assuming there's a health component on the player GameObject
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }

            // Set a cooldown for the attack to prevent rapid attacks
            isAttacking = true;
            Invoke("ResetAttack", 1f); // Adjust the cooldown time as needed
            audioManager.PlaySFX(audioManager.TreeAttack);

        }
    }

    void ResetAttack()
    {
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the attack range in the Unity editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}