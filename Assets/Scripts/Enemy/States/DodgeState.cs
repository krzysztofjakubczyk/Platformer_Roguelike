using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : State
{
    protected DodgeStateData stateData;

    protected bool performCloseRangeAction;
    protected bool isPlayerInMaxAgroRange;
    protected bool isGrounded;
    protected bool isDodgeOver;
    protected bool isDodgeCooldownOver;
    protected bool isPlayerInMinAgroRange;
    public DodgeState(Entity entity, BaseStateMachine stateMachine, string animBoolName, DodgeStateData stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
        isGrounded = entity.CheckGround();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();

    }

    public override void Enter()
    {
        base.Enter();
        isDodgeOver = false;
        isDodgeCooldownOver = false;
        entity.SetVelocity(stateData.dodgeSpeed, stateData.dodgeAngle, -entity.facingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.dodgeTime && isGrounded) isDodgeOver=true;
        if (Time.time >= startTime + stateData.dodgeTime + stateData.dodgeCooldown && isGrounded) isDodgeCooldownOver = true;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
