using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingRangeEnemyIdleState : IdleState
{
    private FlyingRangeEnemy enemy;
    public FlyingRangeEnemyIdleState(Entity entity, BaseStateMachine stateMachine, string animBoolName, IdleStateData stateData, FlyingRangeEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        base.LogicUpdate();
        if (isPlayerInMinAgroRange)
        {
            //stateMachine.ChangeState(enemy.playerDetectedState);
        }
        if (isIdleTimeOver)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
