using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikerMeleeAttackState :MeleeAttackState
{
    private Striker enemy;
    private int comboCounter = 0;
    public StrikerMeleeAttackState(Entity entity, BaseStateMachine stateMachine, string animBoolName, Transform attackPosition, MeleeAttackStateData stateData, Striker enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
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
            {
            if (isPlayerInMaxAgroRange) {
                stateMachine.ChangeState(enemy.rangedAttackState);
                //stateMachine.ChangeState(enemy.playerDetectedState);
            }
                else stateMachine.ChangeState(enemy.lookForPlayerState);
            }   
            
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        comboCounter++;

        if (comboCounter == 3) comboCounter = 1;
        if (comboCounter == 1)
        {
            Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);
            entity.audioSource.clip = stateData.audioStab;
            entity.audioSource.Play();
            foreach (Collider2D collider in detectedObjects)
            {
                Debug.Log(entity.playerTransform);
                entity.playerHp.DamagePlayer(attackDetails.damageAmount);
                entity.playerTransform.gameObject.GetComponent<Rigidbody2D>().AddForce(stateData.vectorPush * stateData.pushForce, ForceMode2D.Impulse);
            }
        }
        else if (comboCounter == 2)
        {
            entity.audioSource.clip = stateData.audioSlash;
            entity.audioSource.Play();
            Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);
            foreach (Collider2D collider in detectedObjects)
            {
                entity.playerTransform.gameObject.GetComponent<MovementFin>().DamageOnCollision(collider);

                entity.playerHp.DamagePlayer(attackDetails.damageAmount);
                Vector2 force = new Vector2(1, 0) * entity.facingDirection * stateData.pushForce * 2;
                Debug.Log("Applying force: " + force);
                entity.playerTransform.gameObject.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
            }
        }

    }
}
