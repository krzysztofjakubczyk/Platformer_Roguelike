using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_ChargeState : Charge
{
    Enemy1 enemy;

    public E1_ChargeState(Entity entity, BaseStateMachine stateMachine, string animBoolName, D_ChargeState stateData,Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(!isDetectingLedge || isDetectingWall)
        {
            //TODO: connect to look for player
        }
        else if (isChargeTimeOver)
        {
            if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
