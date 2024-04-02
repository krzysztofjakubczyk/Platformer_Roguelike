using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ER_PlayerDetectedState : PlayerDetected
{
    private EnemyRange enemy;

    public ER_PlayerDetectedState(Entity entity, BaseStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, EnemyRange enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        Debug.Log("czy widzi przeciwnika w melee" + performCloseRangeAction);
        if (performCloseRangeAction) stateMachine.ChangeState(enemy.meleeAttackState);
        else if (isPlayerInMaxAgroRange) stateMachine.ChangeState(enemy.lookForPlayerState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
