using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Animator animator;
    public Transform player;

    public enum EnemyState
    {
        Idle,
        Patrol,
        Chase,
        Attack
    }

    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float attackRange = 2f;
    public float detectionRadius = 10f;

    private EnemyState currentState = EnemyState.Idle;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                // Implement idle behavior (optional)
                animator.SetBool("IsWalking", false); // Stop walking animation
                break;
            case EnemyState.Patrol:
                Patrol();
                break;
            case EnemyState.Chase:
                Chase();
                break;
            case EnemyState.Attack:
                Attack();
                break;
        }
    }

    void Patrol()
    {
        // Implement patrol behavior (e.g., move between waypoints)
        // Example: transform.Translate(Vector3.forward * patrolSpeed * Time.deltaTime);

        // Check if player is within detection range
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            currentState = EnemyState.Attack;
        }
        else if (Vector3.Distance(transform.position, player.position) <= detectionRadius)
        {
            currentState = EnemyState.Chase;
        }
        else
        {
            animator.SetBool("IsWalking", true); // Start walking animation
        }
    }

    void Chase()
    {
        // Move towards the player
        transform.LookAt(player);
        transform.Translate(Vector3.forward * chaseSpeed * Time.deltaTime);

        // Check if player is within attack range
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            currentState = EnemyState.Attack;
        }
        else
        {
            animator.SetBool("IsWalking", true); // Start walking animation
        }
    }

    void Attack()
    {
        // Implement attack behavior (e.g., deal damage to the player)
        // You may want to use a separate script for handling attack logic

        // Check if player is out of attack range
        if (Vector3.Distance(transform.position, player.position) > attackRange)
        {
            currentState = EnemyState.Chase;
        }
        else
        {
            // Trigger attack animation
            animator.SetTrigger("Attack");
        }
    }
}