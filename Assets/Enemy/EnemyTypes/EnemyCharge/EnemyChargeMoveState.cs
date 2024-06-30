using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChargeMoveState : MoveState
{
    private EnemyCharge enemy;
    public EnemyChargeMoveState(Entity entity, BaseStateMachine stateMachine, string animBoolName, MoveStateData stateData, EnemyCharge enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if (isEnemyInRange)
        {
            enemy.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.idleState);
        }
        else if (isPlayerInMinAgroRange && !isEnemyInRangeToCharge)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        else if(isDetectingWall || !isDetectingLedge)
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
