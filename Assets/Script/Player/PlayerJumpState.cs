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

        player.jumpTimeCounter = player.jumpTime; // ��ʼ����Ծʱ��
        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
        player.rb.gravityScale = player.defaultGravityScale; // �ָ�Ĭ������
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
            player.jumpTimeCounter = 0; // ������Ծʱ��
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

        // ������Ծ�߼�
        if (Input.GetKey(KeyCode.K))
        {
            // ������ʱ��С���ض�ֵʱ�����ּ��ٶȲ���
            if (player.jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, player.jumpForce); // ������Ծ���ٶȲ���

                // ���ټ�ʱ��
                player.jumpTimeCounter -= Time.deltaTime;

                // ����ʱ�������ض���ֵʱ����ʼ���ټ��ٶ�
                if (player.jumpTimeCounter < player.minJumpTime)
                {
                    // ���ټ��ٶȣ�����������С�������ٶ�
                    player.rb.gravityScale = Mathf.Max(Mathf.Lerp(player.gravityScale, player.minGravityScale, (player.minJumpTime - player.jumpTimeCounter) / player.minJumpTime), player.minGravityScale);
                }
            }
        }
        else
        {
            // ������Ծʱ��
            player.jumpTimeCounter = 0;
            stateMachine.ChangeState(player.airState);
        }

        // ����Ƿ���Ҫ����״̬
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
