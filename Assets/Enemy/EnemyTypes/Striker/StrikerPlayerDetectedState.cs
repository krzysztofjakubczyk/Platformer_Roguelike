using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikerPlayerDetectedState : PlayerDetected
{
    private Striker enemy;
    public StrikerPlayerDetectedState(Entity entity, BaseStateMachine stateMachine, string animBoolName, PlayerDetectedData stateData, Striker enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        if (isEnemyInRange)
        {
            entity.Flip();
            stateMachine.ChangeState(enemy.moveState);
        }
        else if (performCloseRangeAction) stateMachine.ChangeState(enemy.meleeAttackState);
        //else if (!isEnemyInRangeToCharge && performCloseRangeAction) stateMachine.ChangeState(enemy.dodgeState);
        else if (!isEnemyInRangeToCharge && performLongRangeAction) stateMachine.ChangeState(enemy.dodgeState);
        //else if (isPlayerInMaxAgroRange && performLongRangeAction) stateMachine.ChangeState(enemy.rangedAttackState);
        else if (!isPlayerInMaxAgroRange) stateMachine.ChangeState(enemy.lookForPlayerState);
        else if (!isDetectingLedge)
        {
            entity.Flip();
            stateMachine.ChangeState(enemy.moveState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
