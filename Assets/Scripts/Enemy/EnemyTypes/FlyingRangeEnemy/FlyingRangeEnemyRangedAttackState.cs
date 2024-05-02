using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingRangeEnemyRangedAttackState : RangedAttackState
{
    private FlyingRangeEnemy enemy;

    public FlyingRangeEnemyRangedAttackState(Entity entity, BaseStateMachine stateMachine, string animBoolName, Transform attackPosition, RangedAttackStateData stateData, FlyingRangeEnemy enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
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

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
            if (isPlayerInMinAgrRange) stateMachine.ChangeState(enemy.playerDetectedState);
            //else stateMachine.ChangeState(enemy.lookForPlayerState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        //base.TriggerAttack();
        Vector3 playerPos = new Vector3(entity.playerTransform.position.x, entity.playerTransform.position.y + 1, entity.playerTransform.position.z);

        // Aim bullet in player's direction.
        attackPosition.rotation = Quaternion.LookRotation(playerPos);

        projectile = GameObject.Instantiate(stateData.projectile, attackPosition.position, attackPosition.rotation);
        projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.FireProjectile(stateData.projectileSpeed, stateData.projectileTravelDistance, stateData.projectileDamage);
    }
}
