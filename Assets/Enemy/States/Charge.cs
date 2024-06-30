using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : State
{
    protected ChargeStateData stateData;
    protected bool isPlayerInMinAgroRange;
    protected bool isEnemyInMinAgroRange;
    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    protected bool isChargeTimeOver;
    protected bool performCloseRangeAction;
    public Charge(Entity entity, BaseStateMachine stateMachine, string animBoolName, ChargeStateData stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isEnemyInMinAgroRange = entity.CheckEnemyInRange();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        isChargeTimeOver = false;
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.chargeTime) isChargeTimeOver = true;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        entity.SetVelocity(stateData.chargeSpeed);
    }
}
