using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;
    private static readonly int IsWalkingHash = Animator.StringToHash("IsWalking");
    private static readonly int IsAttackingHash = Animator.StringToHash("IsAttacking");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetWalkingAnimation(bool isWalking)
    {
        animator.SetBool(IsWalkingHash, isWalking);
    }

    public void SetAttackingAnimation(bool isAttacking)
    {
        animator.SetBool(IsAttackingHash, isAttacking);
    }

    public void PlayAttackAnimation()
    {
        animator.SetTrigger(IsAttackingHash);
    }
}