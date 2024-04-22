using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private AudioManager audioManager; // Add AudioManager reference

    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        audioManager.PlaySFX(audioManager.Attack);
        Debug.Log("Attack parameter set to true");

        /*audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        if (audioManager != null)
        {
            audioManager.PlaySFX(audioManager.Attack);
        }*/
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Attack parameter set to true");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void AttackTrigger()
    {
        player.AttackTrigger();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        isAbilityDone = true;
    }
}
