using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_PlayerDetectedState : PlayerDetected
{
    private Enemy1 enemy;
    public E1_PlayerDetectedState(Entity entity, BaseStateMachine stateMachine, string animBoolName, PlayerDetectedData stateData,Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
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
        
       if(isEnemyInRange)
        {
            entity.Flip();
            stateMachine.ChangeState(enemy.moveState);
        }
        else if (performCloseRangeAction) stateMachine.ChangeState(enemy.meleeAttackState);
        else if (!isEnemyInRangeToCharge && performLongRangeAction) stateMachine.ChangeState(enemy.chargeState);
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
