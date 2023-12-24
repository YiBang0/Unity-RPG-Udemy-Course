using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerWallslideState : PlayerState
{
    public playerWallslideState(Player _player, PlayerStateMashine _stateMashine, string _animBoolName) : base(_player, _stateMashine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {

        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (!player.IsWallDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            stateMachine.ChangeState(player.wallJump);
            return;
        }
        if (xInput != 0 && player.facingDir != xInput)
        {
            stateMachine.ChangeState(player.idleState);
        }

        if(yInput < 0)
        {
            rb.velocity = new Vector2 (0, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y * 0.7f);
        }

        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }

        

    }

}
