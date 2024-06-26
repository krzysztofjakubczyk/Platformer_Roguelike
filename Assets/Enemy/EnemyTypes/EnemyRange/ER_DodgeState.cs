using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ER_DodgeState : DodgeState
{
    private EnemyRange enemy;
    public ER_DodgeState(Entity entity, BaseStateMachine stateMachine, string animBoolName, DodgeStateData stateData, EnemyRange enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        if (isDodgeOver)
            if (isPlayerInMaxAgroRange && performCloseRangeAction) stateMachine.ChangeState(enemy.meleeAttackState);
            else if (isPlayerInMaxAgroRange && !performCloseRangeAction) stateMachine.ChangeState(enemy.rangedAttackState);
            else if(!isPlayerInMaxAgroRange)stateMachine.ChangeState(enemy.lookForPlayerState);
}

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
