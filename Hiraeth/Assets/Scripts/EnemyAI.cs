using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float sightRange = 5f;
    public float patrolRadius = 2f;
    public float attackRange = 2f;
    public LayerMask playerLayer;

    public float idleTime = 2f; // Time to idle before resuming patrolling
    private float idleTimer = 0f; // Timer for idle state

    public Transform target;

    private Vector2 startingPosition;
    private Vector2 patrolDestination;
    private EnemyAnimation enemyAnimator;
    private EnemyAttack enemyAttack;

    private bool isPatrolling = true;
    private bool isChasing = false;
    private bool isAttacking = false;

    private void Awake()
    {
        enemyAnimator = GetComponent<EnemyAnimation>();
        enemyAttack = GetComponent<EnemyAttack>();
        startingPosition = transform.position;
        SetNextPatrolDestination();
    }

    private void Update()
    {
        if (isChasing)
        {
            ChaseTarget();
        }
        else if (isPatrolling)
        {
            Patrol();
        }
        else if (isAttacking)
        {
            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        bool isWalking = (target != null && Vector2.Distance(transform.position, target.position) > 0.1f);
        enemyAnimator.SetWalking(isWalking);

        if (isWalking)
        {
            // Determine movement direction
            Vector2 direction = (target.position - transform.position).normalized;

            // Flip the enemy sprite along the x-axis based on movement direction
            if (direction.x > 0)
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            else if (direction.x < 0)
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            // Move towards the target
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, target.position) <= attackRange)
        {
            isChasing = false;
            isAttacking = true;
        }
    }

    private void Patrol()
    {
        bool isWalking = (Vector2.Distance(transform.position, patrolDestination) > 0.1f);
        enemyAnimator.SetWalking(isWalking);

        if (isWalking)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolDestination, moveSpeed * Time.deltaTime);
        }
        else
        {
            // If idle, wait for the idleTime duration before patrolling again
            if (idleTimer <= 0)
            {
                SetNextPatrolDestination();
                idleTimer = idleTime; // Reset the idle timer
            }
            else
            {
                idleTimer -= Time.deltaTime;
                // Set the enemy to idle animation here
                enemyAnimator.SetIdle(true);
            }
        }

        if (Vector2.Distance(transform.position, target.position) <= sightRange)
        {
            isPatrolling = false;
            isChasing = true;
        }
    }

    private void AttackTarget()
    {
        enemyAnimator.SetAttacking(true);

        // Check for player within attack range
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);
        foreach (Collider2D player in hitPlayers)
        {
            // Attack the player
            enemyAttack.AttackPlayer(player.gameObject);
            enemyAnimator.SetAttacking(false);
        }

        // Reset the attack state after a short delay
        Invoke("ResetAttackState", 1f);
    }

    private void ResetAttackState()
    {
        isAttacking = false;
        if (Vector2.Distance(transform.position, target.position) <= sightRange)
        {
            isChasing = true;
        }
        else
        {
            isPatrolling = true;
        }
    }

    private void SetNextPatrolDestination()
    {
        Vector2 randomDirection = Random.insideUnitCircle * patrolRadius;
        patrolDestination = startingPosition + randomDirection;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}