using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingRangeEnemyDeathState : DeathState
{
    private FlyingRangeEnemy enemy;

    public FlyingRangeEnemyDeathState(Entity entity, BaseStateMachine stateMachine, string animBoolName, DeathStateData stateData, FlyingRangeEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
