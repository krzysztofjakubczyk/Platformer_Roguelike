using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikerMoveState : MoveState
{
    private Striker enemy;
    public StrikerMoveState(Entity entity, BaseStateMachine stateMachine, string animBoolName, MoveStateData stateData, Striker enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
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
        Debug.Log("wall" + isDetectingWall);
        Debug.Log("podloga" + isDetectingLedge);
        base.LogicUpdate();
        if (isEnemyInRange)
        {
            enemy.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.idleState);
        }
        else if (isPlayerInMinAgroRange && !isEnemyInRangeToCharge)
        {
            //stateMachine.ChangeState(enemy.playerDetectedState);
        }
        else if (isDetectingWall || !isDetectingLedge)
        {
            enemy.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
