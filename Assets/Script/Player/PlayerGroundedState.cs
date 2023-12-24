using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMashine _stateMashine, string _animBoolName) : base(_player, _stateMashine, _animBoolName)
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

        if(Input.GetKeyDown(KeyCode.R))
        {
            stateMachine.ChangeState(player.blackHole);
        }

        if(Input.GetKeyDown(KeyCode.I)&& HasNoSword())
        {
            stateMachine.ChangeState(player.aimSword);
        }

        if(Input.GetKeyDown(KeyCode.U) && SkillManager.instance.counter.CanUseSkill())
        {
            stateMachine.ChangeState(player.counterAttack);
        }

        if(Input.GetKeyDown(KeyCode.J))
        {
            stateMachine.ChangeState(player.primaryAttack);
        }

        if(!player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.airState);
        }

        if(Input.GetKeyDown(KeyCode.K)&&player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.jumpState);    
        }
    }

    private bool HasNoSword()
    {
        if (!player.sword)
        {
            return true;
        }

        player.sword.GetComponent<Sword_Skill_Controller>().ReturnSword();
        return false;
    }

}
