using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikerDeathState : DeathState
{
    private Striker enemy;

    public StrikerDeathState(Entity entity, BaseStateMachine stateMachine, string animBoolName, DeathStateData stateData, Striker enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        Debug.Log("Umiera Striker");
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
