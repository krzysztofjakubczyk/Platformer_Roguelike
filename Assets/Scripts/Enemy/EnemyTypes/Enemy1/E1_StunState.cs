using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_StunState :StunState
{
    Enemy1 enemy;

    public E1_StunState(Entity entity, BaseStateMachine stateMachine, string animBoolName, D_StunStateData stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
