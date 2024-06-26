using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToPatrolState : State
{
    protected BackToPatrolStateData stateData;
    public BackToPatrolState(Entity entity, BaseStateMachine stateMachine, string animBoolName, BackToPatrolStateData stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
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
        CheckFlipToPoint();
        Vector2 targetPosition = new Vector2((float)Math.Round(entity.patrolPoint.x, 1), (float)Math.Round(entity.patrolPoint.y, 1));

        entity.aliveGameObject.transform.localPosition = Vector2.MoveTowards(
            entity.aliveGameObject.transform.localPosition,
            new Vector2(targetPosition.x, targetPosition.y),
            stateData.backSpeed * Time.deltaTime
        );
    }
    public virtual void CheckFlipToPoint()
    {
        if (entity.aliveGameObject.transform.position.x < entity.patrolPoint.x)
        {
            if (entity.facingDirection != 1) entity.Flip();
        }
        else if (entity.aliveGameObject.transform.position.x > entity.patrolPoint.x)
        {
            if (entity.facingDirection == 1) entity.Flip();
        }
    }
}
