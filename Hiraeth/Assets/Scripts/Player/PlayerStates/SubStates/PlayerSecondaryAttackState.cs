using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSecondaryAttackState : PlayerAbilityState
{

    //private AudioManager audioManager;

    public PlayerSecondaryAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        //audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    public override void Enter()
    {
        base.Enter();

       // audioManager.PlaySFX(audioManager.SecondAttack);
        Debug.Log("2nd Attack parameter set to true");
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

    private void TriggerAttack()
    {
        player.AttackTrigger();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        isAbilityDone = true;
    }
}