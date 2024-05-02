using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingRangeEnemyPlayerDetectedState : PlayerDetected
{
    private FlyingRangeEnemy enemy;

    public FlyingRangeEnemyPlayerDetectedState(Entity entity, BaseStateMachine stateMachine, string animBoolName, PlayerDetectedData stateData, FlyingRangeEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        if (entity.aliveGameObject.transform.position.x < entity.playerTransform.position.x)
        {
            if (entity.facingDirection != 1) entity.Flip();


        }
        else if (entity.aliveGameObject.transform.position.x > entity.playerTransform.position.x)
        {
            if (entity.facingDirection == 1) entity.Flip();
        }
        //if (performCloseRangeAction) stateMachine.ChangeState(enemy.meleeAttackState);
        if (performLongRangeAction) stateMachine.ChangeState(enemy.rangedAttackState);
        else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
