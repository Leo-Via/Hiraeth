using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerAttackState PrimaryAttackState { get; private set; }
    public PlayerSecondaryAttackState SecondaryAttackState { get; private set; }
    [SerializeField]
    private PlayerData playerData;
    #endregion

    #region Components
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    #endregion

    #region Check Transforms

    [SerializeField]
    private Transform groundCheck;
    public Transform attackPosition;

    #endregion

    #region Other Variables
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }

    private Vector2 workspace;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
        SecondaryAttackState = new PlayerSecondaryAttackState(this, StateMachine, playerData, "secondaryAttack");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();

        FacingDirection = 1;

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        CurrentVelocity = RB.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Set Functions
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    #endregion

    #region Check Functions

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    #endregion

    #region Other Functions

    public void AttackTrigger()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, playerData.attackRadius, playerData.attackLayer);

        foreach (Collider2D collider in detectedObjects)
        {
            if (collider.gameObject.name == "Alive")
            {
                Entity enemy = collider.gameObject.GetComponentInParent<Entity>();
                if (enemy != null)
                {
                    AttackDetails attackDetails = new AttackDetails
                    {
                        damageAmount = playerData.attackDamage,
                        stunDamageAmount = playerData.stunDamageAmount,
                        position = transform.position
                    };

                    enemy.Damage(attackDetails);
                }
            }
        }
    }

    public void Damage(AttackDetails attackDetails)
    {
        // Handle the damage logic for the player
        // You can reduce the player's health, apply knockback, etc.
        Debug.Log("Player received damage: " + attackDetails.damageAmount);

        // Example of reducing player's health
        float currentHealth = 100f; // Replace with your player's current health
        currentHealth -= attackDetails.damageAmount;

        // Example of applying knockback
        Vector2 knockbackDirection = (Vector2)transform.position - attackDetails.position;
        knockbackDirection.Normalize();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(knockbackDirection * 10f, ForceMode2D.Impulse);

        // Additional damage handling logic...
    }


    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimtionFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    #endregion
}