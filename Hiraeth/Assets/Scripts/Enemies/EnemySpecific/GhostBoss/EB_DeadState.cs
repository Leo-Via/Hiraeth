using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EB_DeadState : DeadState
{
    private EnemyBoss enemy;

    public EB_DeadState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData, EnemyBoss enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
