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
        enemyAnimator.SetWalkingAnimation(isWalking);

        if (isWalking)
        {
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
        enemyAnimator.SetWalkingAnimation(isWalking);

        if (isWalking)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolDestination, moveSpeed * Time.deltaTime);
        }
        else
        {
            SetNextPatrolDestination();
        }

        if (Vector2.Distance(transform.position, target.position) <= sightRange)
        {
            isPatrolling = false;
            isChasing = true;
        }
    }

    private void AttackTarget()
    {
        enemyAnimator.SetAttackingAnimation(true);

        // Check for player within attack range
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);
        foreach (Collider2D player in hitPlayers)
        {
            // Attack the player
            enemyAttack.AttackPlayer(player.gameObject);
            enemyAnimator.PlayAttackAnimation();
        }

        // Reset the attack state after a short delay
        Invoke("ResetAttackState", 1f);
    }

    private void ResetAttackState()
    {
        isAttacking = false;
        enemyAnimator.SetAttackingAnimation(false);
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