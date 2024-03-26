using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : State
{
    protected D_StunStateData stateData;
    
    protected bool isStunTimeOver;
    protected bool isGrounded;
    protected bool isMovementStopped;
    public StunState(Entity entity, BaseStateMachine stateMachine, string animBoolName, D_StunStateData stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = entity.CheckGround();
    }

    public override void Enter()
    {
        base.Enter();
        isStunTimeOver = false;
        isMovementStopped = false;
        entity.SetVelocity(stateData.stunKnockbackSpeed, stateData.stunKnockbackAngle, entity.lastDamageDirection);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time>= startTime + stateData.stunTime) isStunTimeOver = true;
        if (isGrounded && Time.time > startTime + stateData.stunKnockbackTime && !isMovementStopped)
        {
            isMovementStopped = true;
            entity.SetVelocity(0f);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
