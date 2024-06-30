using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangePlayerDetectedState : PlayerDetected
{
    private EnemyRange enemy;

    public EnemyRangePlayerDetectedState(Entity entity, BaseStateMachine stateMachine, string animBoolName, PlayerDetectedData stateData, EnemyRange enemy) : base(entity, stateMachine, animBoolName, stateData)
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
            if (Time.time >= enemy.dodgeState.startTime + enemy.dodgeStateData.dodgeCooldown)
                stateMachine.ChangeState(enemy.dodgeState);
            else stateMachine.ChangeState(enemy.meleeAttackState);
        }
        else if (performLongRangeAction) stateMachine.ChangeState(enemy.rangedAttackState);
        else if (!isPlayerInMaxAgroRange) stateMachine.ChangeState(enemy.lookForPlayerState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
