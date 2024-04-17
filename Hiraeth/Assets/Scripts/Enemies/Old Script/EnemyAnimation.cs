using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;

    private static readonly string SpeedParameter = "Velocity";
    private static readonly string IsAttackingParameter = "IsAttacking";
    private static readonly string IsHitParameter = "IsHurt";
    private static readonly string IsDeadParameter = "IsDead";

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetSpeed(float speed)
    {
        animator.SetFloat(SpeedParameter, speed);
    }

    public void Attack()
    {
        animator.SetBool(IsAttackingParameter, true);
    }

    public void GetHit()
    {
        animator.SetTrigger(IsHitParameter);
    }

    public void Die()
    {
        animator.SetBool(IsDeadParameter, true);
    }
}

