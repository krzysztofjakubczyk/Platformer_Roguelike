using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FE_IdleState : IdleState
{
    FlyingEnemy enemy;
    public FE_IdleState(Entity entity, BaseStateMachine stateMachine, string animBoolName, D_IdleState stateData, FlyingEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        if (isIdleTimeOver)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
