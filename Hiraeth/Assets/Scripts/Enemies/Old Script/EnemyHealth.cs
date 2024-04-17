using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Animator animator;
    public EnemyAnimation enemyAnimation;

    public int maxHealth = 100; // Maximum health of the enemy
    public int currentHealth;   // Current health of the enemy

    private EnemyAI enemyAI;

    // Optional: You can add an event or delegate to be triggered when the enemy is defeated
    //public delegate void EnemyDefeated();
    //public static event EnemyDefeated OnEnemyDefeated;

    void Start()
    {
        animator = GetComponent<Animator>();
        enemyAnimation = GetComponent<EnemyAnimation>();
        enemyAI = GetComponent<EnemyAI>();
        currentHealth = maxHealth; // Set current health to max health when the enemy is spawned
    }

    // Method to handle damage taken by the enemy
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce current health by the amount of damage taken

        if (enemyAnimation != null)
        {
            enemyAnimation.GetHit();
        }

        // Check if the enemy has been defeated
        if (currentHealth <= 0)
        {
            Die(); // Call the Die() method if the enemy has no health remaining
        }

        Debug.Log("NEw Enemy health: " + currentHealth);
    }

    /*private void SnapToGround()
    {
        if (enemyAI.isGrounded)
        {
            Collider2D groundCollider = Physics2D.OverlapCircle(transform.position, 0.1f, enemyAI.groundLayer);
            if (groundCollider != null)
            {
                transform.position = new Vector3(transform.position.x, groundCollider.transform.position.y, transform.position.z);
            }
        }
    }*/


    // Method to handle the enemy's death
    void Die()
    {
        enemyAnimation.Die();

        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EnemyAI>().enabled = false;

        this.enabled = false;

        // Disable the Rigidbody2D component
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.simulated = false;
        rb.isKinematic = true;

        // Optionally, you can set the enemy's position to the ground level
        //SnapToGround();

        // Destroy the enemy GameObject
        //Destroy(gameObject);
    }
}