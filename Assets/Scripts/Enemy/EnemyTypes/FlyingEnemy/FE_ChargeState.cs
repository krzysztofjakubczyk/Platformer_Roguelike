using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FE_ChargeState : Charge
{
    private FlyingEnemy enemy;
    public FE_ChargeState(Entity entity, BaseStateMachine stateMachine, string animBoolName, D_ChargeState stateData, FlyingEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(enemy.meleeAttackState);
        }
        else if (isDetectingWall)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
        else if (isChargeTimeOver)
        {
            if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        Debug.Log(entity.playerTransform.position);
        Vector2 direction = (entity.playerTransform.position - entity.aliveGameObject.transform.position);
        float lengthToPlayer = direction.magnitude;
        Debug.Log(lengthToPlayer);
        if (lengthToPlayer < stateData.distanceToStop) entity.SetVelocity(0f);
        else {
            Debug.Log("player" + entity.playerTransform.position.x + "enemy" + entity.transform.position.x);
            if (entity.aliveGameObject.transform.position.x < entity.playerTransform.position.x)
            {
                if (entity.facingDirection != 1) entity.Flip();
                entity.SetVelocity(stateData.chargeSpeed, direction, entity.facingDirection);
            }
            else if(entity.aliveGameObject.transform.position.x > entity.playerTransform.position.x)
            {
                entity.SetVelocity(stateData.chargeSpeed, direction, -entity.facingDirection);
            }
               
        }
    }
}
