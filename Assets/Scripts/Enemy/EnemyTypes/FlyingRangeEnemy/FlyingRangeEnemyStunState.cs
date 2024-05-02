using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingRangeEnemyStunState : StunState
{
    private FlyingRangeEnemy enemy;

    public FlyingRangeEnemyStunState(Entity entity, BaseStateMachine stateMachine, string animBoolName, StunStateData stateData, FlyingRangeEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        if (isStunTimeOver)
            if (isPlayerInMinAgroRange) stateMachine.ChangeState(enemy.playerDetectedState);
            else stateMachine.ChangeState(enemy.lookForPlayerState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
