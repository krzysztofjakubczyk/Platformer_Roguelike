using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingRangeEnemyBackToPatrolState : BackToPatrolState
{
    private FlyingRangeEnemy enemy;

    public FlyingRangeEnemyBackToPatrolState(Entity entity, BaseStateMachine stateMachine, string animBoolName, BackToPatrolStateData stateData, FlyingRangeEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        if (entity.aliveGameObject.transform.position == stateData.patrolPoint) stateMachine.ChangeState(enemy.moveState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
