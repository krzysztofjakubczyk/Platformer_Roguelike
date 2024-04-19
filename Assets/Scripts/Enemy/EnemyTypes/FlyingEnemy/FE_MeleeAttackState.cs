using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FE_MeleeAttackState : MeleeAttackState
{
    private FlyingEnemy enemy;
    public FE_MeleeAttackState(Entity entity, BaseStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttack stateData, FlyingEnemy enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        entity.rb.constraints = RigidbodyConstraints2D.FreezePositionY;
    }

    public override void Exit()
    {
        base.Exit();
        entity.rb.constraints = RigidbodyConstraints2D.None;
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            if (isPlayerInMinAgrRange) stateMachine.ChangeState(enemy.playerDetectedState);
            else stateMachine.ChangeState(enemy.lookForPlayerState);
        }
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
