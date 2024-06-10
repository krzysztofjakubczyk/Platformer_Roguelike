using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikerMoveState : MoveState
{
    private Striker enemy;
    public StrikerMoveState(Entity entity, BaseStateMachine stateMachine, string animBoolName, MoveStateData stateData, Striker enemy) : base(entity, stateMachine, animBoolName, stateData)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
