using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingRangeEnemyLookForPlayerState : LookForPlayerState
{
    private FlyingRangeEnemy enemy;

    public FlyingRangeEnemyLookForPlayerState(Entity entity, BaseStateMachine stateMachine, string animBoolName, LookForPlayerStateData stateData, FlyingRangeEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        else if (isAllTurnsTimeDone) stateMachine.ChangeState(enemy.backToPatrolState); //mo¿e pó¿niej dodam back to patrol state
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
