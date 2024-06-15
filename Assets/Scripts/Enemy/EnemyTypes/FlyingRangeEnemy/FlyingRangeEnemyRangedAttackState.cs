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
            if (isPlayerInMinAgroRange) stateMachine.ChangeState(enemy.playerDetectedState);
            else stateMachine.ChangeState(enemy.lookForPlayerState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        //base.TriggerAttack();
        Vector2 playerPos = entity.playerTransform.position - entity.aliveGameObject.transform.position;
        Vector2 playerRot = entity.aliveGameObject.transform.position - entity.playerTransform.position;

        float angle = Mathf.Atan2(playerRot.y, playerRot.x) * Mathf.Rad2Deg; // Calculate the angle between the attack position and the player position

        attackPosition.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // Rotate the attack position towards the player position

        projectile = GameObject.Instantiate(stateData.projectile, attackPosition.position, attackPosition.rotation);
        projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.FireProjectile(stateData.projectileSpeed, stateData.projectileTravelDistance, stateData.projectileDamage, playerPos,entity);
    }
}
