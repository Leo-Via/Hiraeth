using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;

    // Animator parameters
    private static readonly string IsWalking = "IsWalking";
    private static readonly string IsAttacking = "IsAttacking";
    private static readonly string IsHit = "IsHit";
    private static readonly string IsDead = "IsDead";
    private static readonly string IsIdle = "IsIdle";

    void Start()
    {
        // Get the Animator component attached to the enemy
        animator = GetComponent<Animator>();
        Debug.Log("Animator: " + animator);
    }

    public void SetWalking(bool isWalking)
    {
        // Set the IsWalking parameter in the animator
        animator.SetBool(IsWalking, isWalking);
        SetIdle(!isWalking); // Set idle based on walking state
    }

    public void SetAttacking(bool isAttacking)
    {
        Debug.Log("SetAttacking: " + isAttacking);
        // Set the IsAttacking parameter in the animator
        animator.SetBool(IsAttacking, isAttacking);
        SetIdle(false); // If attacking, not idle
    }

    public void SetHit(bool isHit)
    {
        // Set the IsHit parameter in the animator
        animator.SetBool(IsHit, isHit);
        SetIdle(false); // If hit, not idle
    }

    public void SetDead(bool isDead)
    {
        // Set the IsDead parameter in the animator
        animator.SetBool(IsDead, isDead);
        SetIdle(false); // If dead, not idle
    }

    public void SetIdle(bool isIdle)
    {
        animator.SetBool(IsIdle, isIdle);
    }
}