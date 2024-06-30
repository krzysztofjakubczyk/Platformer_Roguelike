using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class MeleeAttackState : AttackState
{
    protected MeleeAttackStateData stateData;

    protected AttackDetails attackDetails;
    protected int comboCounter = 0;
    public MeleeAttackState(Entity entity, BaseStateMachine stateMachine, string animBoolName, Transform attackPosition, MeleeAttackStateData stateData) : base(entity, stateMachine, animBoolName, attackPosition)
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

        attackDetails.damageAmount = stateData.attackDamage;
        attackDetails.position = entity.aliveGameObject.transform.position;
        //Debug.Log(entity.playerHp);
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
        //Debug.Log(entity.gameObject.GetComponent<AudioSource>().name);
        //entity.gameObject.GetComponent<AudioSource>().Play();
        
        entity.playerHp.DamagePlayer(attackDetails.damageAmount);
        entity.playerTransform.gameObject.GetComponent<Rigidbody2D>().AddForce(stateData.vectorPush * stateData.pushForce, ForceMode2D.Impulse);
        //entity.pl

    }
}
