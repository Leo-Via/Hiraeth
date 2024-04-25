using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EB_DeadState : DeadState
{
    private EnemyBoss enemy;
    private AudioManager audioManager;


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
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        if (audioManager != null)
        {
            audioManager.PlaySFX(audioManager.BossDeath);
        }
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
