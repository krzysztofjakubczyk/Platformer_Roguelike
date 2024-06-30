using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : AttackState
{
    protected RangedAttackStateData stateData;
    protected GameObject projectile;
    protected Projectile projectileScript;
    public RangedAttackState(Entity entity, BaseStateMachine stateMachine, string animBoolName, Transform attackPosition, RangedAttackStateData stateData) : base(entity, stateMachine, animBoolName, attackPosition)
    {
        this.stateData = stateData;
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
        //entity.audioSource.Play();
        projectile = GameObject.Instantiate(stateData.projectile, attackPosition.position, Quaternion.Euler(0,0,(entity.playerTransform.position - entity.aliveGameObject.transform.position).z));
        projectileScript = projectile.GetComponent<Projectile>();
        
        projectileScript.FireProjectile(stateData.projectileSpeed, stateData.projectileTravelDistance, stateData.projectileDamage, (entity.playerTransform.position - entity.aliveGameObject.transform.position).normalized,entity);
    }
}
