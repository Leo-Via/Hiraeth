using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerState
{
    private LastCheckPoint lastCheckPoint;

    public PlayerDeadState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        lastCheckPoint = LastCheckPoint.instance;

        // Disable player movement and collision
        player.SetVelocityX(0f);
        player.SetVelocityY(0f);
        player.GetComponent<Collider2D>().enabled = false;
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        player.StartCoroutine(RespawnAtLastCheckpoint(2f));
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    private IEnumerator RespawnAtLastCheckpoint(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Enable player movement and collision
        player.GetComponent<Collider2D>().enabled = true;
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        // Set the player's position to the last checkpoint
        player.transform.position = lastCheckPoint.lastCheckPointPos;

        // Reset player health and state
        player.currentHealth = player.MaxHealth;
        stateMachine.ChangeState(player.IdleState);
    }
}
