using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Health")]
    public float maxHealth = 100f;

    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public int amountOfJumps = 1;

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public LayerMask whatIsGround;

    [Header("Attack State")]
    public float attackCooldown = 0.5f;
    public float attackRadius = 1f;
    public LayerMask attackLayer;
    public float attackDamage = 10f;
    public float stunDamageAmount = 0.5f;

    [Header("Knockback")]
    public float knockbackStrength = 5f;
    public float maxKnockbackSpeed = 5f;
    public float counterForceMultiplier = 10f;
}