using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Entity
{
    public EB_IdleState idleState { get; private set; }
    public EB_MoveState moveState { get; private set; }
    public EB_PlayerDetectedState playerDetectedState { get; private set; }
    public EB_ChargeState chargeState { get; private set; }
    public EB_LookForPlayerState lookForPlayerState { get; private set; }
    public EB_MeleeAttackState meleeAttackState { get; private set; }
    public EB_StunState stunState { get; private set; }
    public EB_DeadState deadState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedData;
    [SerializeField]
    private D_ChargeState chargeStateData;
    [SerializeField]
    private D_LookForPlayer lookForPlayerStateData;
    [SerializeField]
    private D_MeleeAttack meleeAttackStateData;
    [SerializeField]
    private D_StunState stunStateData;
    [SerializeField]
    private D_DeadState deadStateData;


    [SerializeField]
    private Transform meleeAttackPosition;

    public override void Start()
    {
        base.Start();

        moveState = new EB_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new EB_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new EB_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        chargeState = new EB_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        lookForPlayerState = new EB_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        meleeAttackState = new EB_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        stunState = new EB_StunState(this, stateMachine, "stun", stunStateData, this);
        deadState = new EB_DeadState(this, stateMachine, "dead", deadStateData, this);

        stateMachine.Initialize(moveState);

    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if (isDead)
        {
            stateMachine.ChangeState(deadState);
            gameObject.tag = "Untagged";

            GameObject aliveObject = transform.Find("Alive").gameObject;
            if (aliveObject != null)
            {
                aliveObject.tag = "Untagged";
            }
            else
            {
                Debug.LogError("Alive child object not found on enemy: " + gameObject.name);
            }
        }
        else if (isStunned && stateMachine.currentState != stunState)
        {
            stateMachine.ChangeState(stunState);
        }
        else if (!CheckPlayerInMinAgroRange())
        {
            lookForPlayerState.SetTurnImmediately(true);
            stateMachine.ChangeState(lookForPlayerState);
        }
    }
}