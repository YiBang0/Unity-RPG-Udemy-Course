using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [Header("Attack details")]
    public float[] attackMovement;
    public float counterAttackDuration = .2f;
    

    public bool isBusy { get; private set; }
    [Header("Move info")]
    public float moveSpeed = 12f;
    public float swordReturnImpact;

    [Header("Dash info")]
    public float dashSpeed;
    public float dashiDuration;
    public float dashDir { get; private set; }

    [Header("Jump info")]
    public float jumpTimeCounter; // ��¼��Ծ�����������µ�ʱ��
    public float jumpTime; // ������Ծ�߶ȵ����ʱ��
    public float jumpForce;
    public float gravityScale; // �������ű���
    public float defaultGravityScale; // Ĭ���������ű���
    public float minGravityScale; // ��С�������ű���
    public float minJumpTime;



    public SkillManager skill {  get; private set; }

    public GameObject sword{  get; private set; }
    
    #region State
    public PlayerStateMashine stateMashine { get; private set; }

    public PlayerIdleState idleState { get; private set; }

    public PlayerMoveState moveState { get; private set; }

    public PlayerJumpState jumpState { get; private set; }

    public PlayerAirState airState { get; private set; }

    public PlayerDashState dashState { get; private set; }

    public playerWallslideState wallslide { get; private set; }

    public playerWallJumpState wallJump { get; private set; }

    public PlayerPrimaryAttackState primaryAttack { get; private set; }

    public PlayerCounterAttackState counterAttack { get; private set; }

    public PlayerAimSwordState aimSword { get; private set; }

    public PlayerCatchSwordState catchSword { get; private set; }

    public PlayerBlackholeState blackHole { get; private set; }

    public PlayerDeadState deadState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        stateMashine = new PlayerStateMashine();

        idleState = new PlayerIdleState(this,stateMashine,"Idle");
        moveState = new PlayerMoveState(this, stateMashine, "Move");
        jumpState = new PlayerJumpState(this, stateMashine, "Jump");
        airState = new PlayerAirState(this, stateMashine, "Jump");
        dashState = new PlayerDashState(this, stateMashine, "Dash");
        wallslide = new playerWallslideState(this, stateMashine, "WallSlide");
        wallJump = new playerWallJumpState(this, stateMashine, "Jump");
        primaryAttack = new PlayerPrimaryAttackState(this, stateMashine, "Attack");
        counterAttack = new PlayerCounterAttackState(this, stateMashine, "CounterAttack");

        aimSword = new PlayerAimSwordState(this, stateMashine, "AimSword");
        catchSword = new PlayerCatchSwordState(this, stateMashine, "CatchSword");
        blackHole = new PlayerBlackholeState(this, stateMashine, "Jump");

        deadState = new PlayerDeadState(this,stateMashine, "Die");
    }

    protected override void Start()
    {

        base.Start();

        skill = SkillManager.instance;

        stateMashine.Initialize(idleState);
        
    }



    protected override void Update()
    {
        base.Update();

        stateMashine.currentState.Update();

        CheckForDashInput();

        if(Input.GetKeyDown(KeyCode.F))
        {
            skill.crystal.CanUseSkill();
        }
    }

    public void AssignNewSword(GameObject _newSword)
    {
        sword = _newSword;
    }

    public void CatchTheSword()
    {
        stateMashine.ChangeState(catchSword);
        Destroy(sword);
    }


    public  IEnumerator Busyfor(float _seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(_seconds);

        isBusy = false;
    }

    public void AnimationTrigger() => stateMashine.currentState.AnimationFinishTrigger(); 

    private void CheckForDashInput()
    {
        if(IsWallDetected())
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.L) && SkillManager.instance.dash.CanUseSkill())
        {
            dashDir = Input.GetAxisRaw("Horizontal");

            if(dashDir == 0)
            {
                dashDir = facingDir;
            }
            stateMashine.ChangeState(dashState);
        }
    }

    public override void Die()
    {
        base.Die();

        stateMashine.ChangeState(deadState);
    }

}
