using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeIdleState : IdleState
{
    private EnemyRange enemy;
    public EnemyRangeIdleState(Entity entity, BaseStateMachine stateMachine, string animBoolName, IdleStateData stateData, EnemyRange enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        if (isPlayerInMinAgroRange) stateMachine.ChangeState(enemy.playerDetectedState);
        else if(isIdleTimeOver) stateMachine.ChangeState(enemy.moveState);  
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
