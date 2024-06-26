using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected MoveStateData stateData;

    protected bool groundCheck;
    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    protected bool isPlayerInMinAgroRange;
    protected bool isEnemyInRange;
    protected bool isEnemyInRangeToCharge;
    public MoveState(Entity entity,BaseStateMachine stateMachine,string animBoolName,MoveStateData stateData): base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        groundCheck = entity.CheckGround();
        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isEnemyInRange = entity.CheckEnemyInRange();
        isEnemyInRangeToCharge = entity.CheckEnemyInRangeToCharge();
}

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(stateData.movementSpeed);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        entity.SetVelocity(stateData.movementSpeed);
    }
}
