using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikerStunState : StunState
{
    private Striker enemy;

    public StrikerStunState(Entity entity, BaseStateMachine stateMachine, string animBoolName, StunStateData stateData, Striker enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        if (isStunTimeOver)
            stateMachine.ChangeState(enemy.moveState); //tymczasowo
        //if (isPlayerInMinAgroRange) stateMachine.ChangeState(enemy.playerDetectedState);
        //    else stateMachine.ChangeState(enemy.lookForPlayerState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
