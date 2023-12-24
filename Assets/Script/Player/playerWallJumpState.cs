using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerWallJumpState : PlayerState
{
    public playerWallJumpState(Player _player, PlayerStateMashine _stateMashine, string _animBoolName) : base(_player, _stateMashine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = .2f;
        player.SetVelocity(2 * -player.facingDir, player.jumpForce+5);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.airState);
        }

        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

}