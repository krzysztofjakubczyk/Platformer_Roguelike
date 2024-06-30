using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeDeathState : DeathState
{
    private EnemyRange enemy;

    public EnemyRangeDeathState(Entity entity, BaseStateMachine stateMachine, string animBoolName, DeathStateData stateData, EnemyRange enemy) : base(entity, stateMachine, animBoolName, stateData)
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
