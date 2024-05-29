using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyBackToPatrolState : BackToPatrolState
{
    private FlyingEnemy enemy;
    public FlyingEnemyBackToPatrolState(Entity entity, BaseStateMachine stateMachine, string animBoolName, BackToPatrolStateData stateData, FlyingEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        if (entity.aliveGameObject.transform.position == entity.patrolPoint) stateMachine.ChangeState(enemy.moveState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
