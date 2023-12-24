using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMashine _stateMashine, string _animBoolName) : base(_player, _stateMashine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.jumpTimeCounter = player.jumpTime; // 初始化跳跃时间
        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
        player.rb.gravityScale = player.defaultGravityScale; // 恢复默认重力
    }

    public override void Update()
    {

        /*player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);
        base.Update();
        if (Input.GetKey(KeyCode.K))
        {
            if (player.jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
                player.jumpTimeCounter -= Time.deltaTime;
            }
        }
        else
        {
            player.jumpTimeCounter = 0; // 重置跳跃时间
            stateMachine.ChangeState(player.airState);
        }
        //player.rb.gravityScale = Mathf.Lerp(player.gravityScale, 1, player.jumpTimeCounter / player.jumpTime);
        player.rb.gravityScale = Mathf.Max(Mathf.Lerp(player.gravityScale, 1, player.jumpTimeCounter / player.jumpTime), player.minGravityScale);
        if (rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.airState);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            stateMachine.ChangeState(player.primaryAttack);
        }*/
        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);
        base.Update();

        // 处理跳跃逻辑
        if (Input.GetKey(KeyCode.K))
        {
            // 当按键时间小于特定值时，保持加速度不变
            if (player.jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, player.jumpForce); // 保持跳跃初速度不变

                // 减少计时器
                player.jumpTimeCounter -= Time.deltaTime;

                // 当计时器低于特定阈值时，开始减少加速度
                if (player.jumpTimeCounter < player.minJumpTime)
                {
                    // 减少加速度，但不低于最小重力加速度
                    player.rb.gravityScale = Mathf.Max(Mathf.Lerp(player.gravityScale, player.minGravityScale, (player.minJumpTime - player.jumpTimeCounter) / player.minJumpTime), player.minGravityScale);
                }
            }
        }
        else
        {
            // 重置跳跃时间
            player.jumpTimeCounter = 0;
            stateMachine.ChangeState(player.airState);
        }

        // 检测是否需要更改状态
        if (rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.airState);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            stateMachine.ChangeState(player.primaryAttack);
        }
    }
}
