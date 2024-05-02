using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingRangeEnemyPlayerDetectedState : PlayerDetected
{
    private FlyingRangeEnemy enemy;

    public FlyingRangeEnemyPlayerDetectedState(Entity entity, BaseStateMachine stateMachine, string animBoolName, PlayerDetectedData stateData, FlyingRangeEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        if (performLongRangeAction) stateMachine.ChangeState(enemy.rangedAttackState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
