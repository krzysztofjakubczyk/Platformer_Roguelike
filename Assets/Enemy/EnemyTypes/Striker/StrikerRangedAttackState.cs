using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikerRangedAttackState : RangedAttackState
{
    private Striker enemy;
    public StrikerRangedAttackState(Entity entity, BaseStateMachine stateMachine, string animBoolName, Transform attackPosition, RangedAttackStateData stateData, Striker enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
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
        entity.audioSource.clip = stateData.spellShot;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        entity.CheckFlipToPlayer();
        if (isAnimationFinished)
            if (isPlayerInMinAgroRange) stateMachine.ChangeState(enemy.playerDetectedState);
            else stateMachine.ChangeState(enemy.lookForPlayerState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
        
    }
}
