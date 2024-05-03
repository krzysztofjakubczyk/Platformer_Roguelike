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
        entity.aliveGameObject.transform.position = Vector3.MoveTowards(entity.aliveGameObject.transform.position, stateData.patrolPoint, stateData.backSpeed * Time.deltaTime);
    }
    public virtual void CheckFlipToPoint()
    {
        if (entity.aliveGameObject.transform.position.x < stateData.patrolPoint.x)
        {
            if (entity.facingDirection != 1) entity.Flip();
        }
        else if (entity.aliveGameObject.transform.position.x > stateData.patrolPoint.x)
        {
            if (entity.facingDirection == 1) entity.Flip();
        }
    }
}
