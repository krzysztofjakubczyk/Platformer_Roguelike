using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikerDodgeState : DodgeState
{
    private Striker enemy;

    public StrikerDodgeState(Entity entity, BaseStateMachine stateMachine, string animBoolName, DodgeStateData stateData, Striker enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        entity.audioSource.clip = stateData.dodgeSound;
        entity.audioSource.Play();
        entity.SetVelocity(stateData.dodgeSpeed, stateData.dodgeAngle, entity.facingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isDodgeOver)
            if (isPlayerInMaxAgroRange && performCloseRangeAction) stateMachine.ChangeState(enemy.meleeAttackState);
            else if (isPlayerInMinAgroRange && isDodgeCooldownOver) stateMachine.ChangeState(enemy.dodgeState);
            else if (!isPlayerInMaxAgroRange) stateMachine.ChangeState(enemy.lookForPlayerState);
            else if (isPlayerInMaxAgroRange) stateMachine.ChangeState(enemy.rangedAttackState);
        entity.CheckFlipToPlayer();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
