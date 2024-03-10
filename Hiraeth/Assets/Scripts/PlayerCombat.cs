using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
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
        // Check for player input to trigger attacks
        if (Input.GetButtonDown("Fire1")) // Adjust "Fire1" to match your attack input button
        {
            if (!isComboActive)
            {
                // Trigger the first attack animation
                Attack();
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

    // Function to trigger the first attack animation
    void Attack()
    {
        // Trigger the first attack animation in the Animator
        animator.SetTrigger(attackTrigger);
    }

    // Function to trigger the second attack animation (combo)
    void Attack2()
    {
        // Trigger the second attack animation (combo) in the Animator
        animator.SetTrigger(attack2Trigger);
    }
}