using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCombat : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int atttackDamage = 40;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    // Reference to the Animator component
    private Animator animator;

    // Parameters in the Animator Controller
    private readonly string attackTrigger = "Attack"; // First attack trigger parameter
    private readonly string attack2Trigger = "Attack2"; // Second attack trigger parameter

    // Variable to track combo state
    private bool isComboActive = false;

    void Start()
    {
        // Get the Animator component attached to the GameObject
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1")) // Adjust "Fire1" to match your attack input button
            {
                if (!isComboActive)
                {
                    // Trigger the first attack animation
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                    isComboActive = true;
                }
                else
                {
                    // Trigger the second attack animation
                    Attack2();
                    isComboActive = false; // Reset combo state after the second attack
                }
            }
        }
    }

    // Function to trigger the first attack animation
    void Attack()
    {
        // Trigger the first attack animation in the Animator
        animator.SetTrigger(attackTrigger);

        // Audio for attacking
        audioManager.PlaySFX(audioManager.Attack);

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(atttackDamage);
            Debug.Log("We hit " + enemy.name);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    // Function to trigger the second attack animation (combo)
    void Attack2()
    {
        // Trigger the second attack animation (combo) in the Animator
        animator.SetTrigger(attack2Trigger);

        // Audio for attacking
        audioManager.PlaySFX(audioManager.Attack);
    }
}