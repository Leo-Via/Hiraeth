using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 2f;
    public float attackDamage = 10f;
    public LayerMask playerLayer;

    private bool isAttacking = false;
    private Coroutine attackCooldownCoroutine;
    private EnemyAnimation enemyAnimation; // Reference to the enemy's animation script

    void Start()
    {
        enemyAnimation = GetComponent<EnemyAnimation>(); // Get the reference to the EnemyAnimation script
    }

    void Update()
    {
        // Only check for player presence if the enemy is not already attacking
        if (!isAttacking)
        {
            Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);

            foreach (Collider2D player in hitPlayers)
            {
                AttackPlayer(player.gameObject);
            }
        }
    }

    public void AttackPlayer(GameObject player)
    {
        // Trigger the attack animation, but don't apply damage directly
        enemyAnimation.Attack();
        StartAttackCooldown();
    }

    void StartAttackCooldown()
    {
        isAttacking = true;
        if (attackCooldownCoroutine != null)
            StopCoroutine(attackCooldownCoroutine);
        attackCooldownCoroutine = StartCoroutine(AttackCooldown());
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(1f); // Adjust cooldown time as needed
        isAttacking = false;
    }

    // This method will be called from the animation event
    public void DealAttackDamage(GameObject player)
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}