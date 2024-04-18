using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FE_LookForPlayerState : LookForPlayerState
{
    private FlyingEnemy enemy;

    public FE_LookForPlayerState(Entity entity, BaseStateMachine stateMachine, string animBoolName, D_LookForPlayerState stateData, FlyingEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        else if (isAllTurnsTimeDone) stateMachine.ChangeState(enemy.moveState);

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
