using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float detectionRange = 5f;
    public float attackRange = 1f;
    public LayerMask playerLayer;

    [Header("Ground Check")]
    public Transform groundCheck; // Assign the ground check transform in the Inspector
    public float groundCheckDistance = 0.2f; // Distance to cast the ground check ray
    public LayerMask groundLayer; // Layer mask for the ground objects
    
    private bool isGrounded = false;


    [Header("Patrol Settings")]
    public Transform[] patrolPoints;
    public float patrolSpeed = 2.0f; // Adjust as needed
    public float patrolWaitTime = 5.0f; // Time to wait at each patrol point
    public float reachThreshold = 0.2f; // Distance threshold to reach a patrol point

    private int currentPatrolPointIndex = 0;
    private bool isPatrolling = false;
    private float patrolTimer = 0.0f;
    private bool isWaiting = false;
    private Vector3 targetPatrolPoint;
    private bool shouldFlip = false;

    private EnemyAttack enemyAttack;
    public EnemyAnimation enemyAnimation;

    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        enemyAnimation = GetComponent<EnemyAnimation>();
        enemyAttack = GetComponent<EnemyAttack>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
            if (CanSeePlayer())
            {
                // Chase or attack player if in range
                if (DistanceToPlayer() <= attackRange)
                {
                    AttackPlayer();
                }
                else
                {
                    ChasePlayer();
                }
            }
            else
            {
                // Do something when player is not in sight
                // If not in sight of the player
                if (!isPatrolling)
                {
                    // Start patrolling
                    StartPatrol();
                }
                else
                {
                    // Continue patrolling
                    Patrol();
                }
            }
        
    }


    private bool CanSeePlayer()
    {
        // Implement your logic to check if the player is in sight
        // For example, you can use Physics2D.Raycast or Physics2D.OverlapCircle
        // Here's a simple example using Physics2D.OverlapCircle
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRange, playerLayer);
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    private void ChasePlayer()
    {
        GroundCheck();
        if (isGrounded)
        {
            // Move towards the player
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;

            Debug.Log("enemy is chasing: " + direction);

            // Flip the enemy sprite if needed
            if (direction.x > 0)
            {
                // If the player is to the right, flip the sprite to face right
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (direction.x < 0)
            {
                // If the player is to the left, flip the sprite to face left
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            // Update the animation speed
            enemyAnimation.SetSpeed(moveSpeed);
        }
    }

    private void AttackPlayer()
    {
        enemyAttack.AttackPlayer(player.gameObject);
    }

    private void StartPatrol()
    {
        // Set the initial patrol point index
        currentPatrolPointIndex = 0;
        isPatrolling = true;
        targetPatrolPoint = patrolPoints[currentPatrolPointIndex].position;
        enemyAnimation.SetSpeed(patrolSpeed); // Reset the animation speed
    }

    private void Patrol()
    {
        if (!isWaiting)
        {
            // Move towards the target patrol point
            transform.position = Vector3.MoveTowards(transform.position, targetPatrolPoint, patrolSpeed * Time.deltaTime);

            // Flip the enemy sprite to face the target patrol point
            FlipSprite((targetPatrolPoint - transform.position).normalized.x);

            // Update the animation speed
            enemyAnimation.SetSpeed(patrolSpeed);

            // Check if we're close enough to the current patrol point
            if (Vector3.Distance(transform.position, targetPatrolPoint) <= reachThreshold)
            {
                // Start waiting at the current patrol point
                isWaiting = true;
                patrolTimer = 0.0f;
                enemyAnimation.SetSpeed(0f); // Set the animation speed to 0 when waiting
            }
        }
        else
        {
            // Wait at the current patrol point
            patrolTimer += Time.deltaTime;
            enemyAnimation.SetSpeed(0f); // Keep the animation speed at 0 while waiting
            if (patrolTimer >= patrolWaitTime)
            {
                // Update the target patrol point to the next one
                currentPatrolPointIndex++;
                if (currentPatrolPointIndex >= patrolPoints.Length)
                {
                    currentPatrolPointIndex = 0;
                }
                targetPatrolPoint = patrolPoints[currentPatrolPointIndex].position;

                isWaiting = false;
                shouldFlip = true;
            }
        }


    }

    private void FlipSprite(float directionX)
    {
        // Flip the enemy sprite along the x-axis based on the movement direction
        // Only flip if the shouldFlip flag is set
        if (shouldFlip || directionX < 0)
        {
            transform.localScale = new Vector3(directionX >= 0 ? Mathf.Abs(transform.localScale.x) : -Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            shouldFlip = false;
            enemyAnimation.SetSpeed(patrolSpeed); // Update the animation speed
        }
    }

    private float DistanceToPlayer()
    {
        return Vector2.Distance(transform.position, player.position);
    }

    private void GroundCheck()
    {
        isGrounded = false;
        // Check if the GroundCheckObject is colliding with other
        // 2D Colliders that are in the "Ground" Layer
        // If yes (isGrounded true) else (isGrounded false)
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckDistance, groundLayer);
        if (colliders.Length > 0)
            isGrounded = true;
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a wire sphere to represent the detection range for the enemy
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Draw the patrol points
        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            Gizmos.color = Color.yellow;
            foreach (Transform point in patrolPoints)
            {
                Gizmos.DrawSphere(point.position, 0.2f);
            }
        }
    }
}