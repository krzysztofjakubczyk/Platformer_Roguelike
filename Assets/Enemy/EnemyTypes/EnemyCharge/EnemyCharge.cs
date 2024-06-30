using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharge : Entity
{
    public EnemyChargeIdleState idleState { get; private set; }
    public EnemyChargeMoveState moveState { get; private set; }
    public EnemyChargePlayerDetectedState playerDetectedState { get; private set; }
    public EnemyChargeChargeState chargeState { get; private set; }
    public EnemyChargeLookForPlayerState lookForPlayerState { get; private set; }
    public EnemyChargeMeleeAttackState meleeAttackState { get; private set; }
    public EnemyChargeStunState stunState { get; private set; }
    public EnemyChargeDeathState deathState { get; private set; }

    [SerializeField]
    private IdleStateData idleStateData;
    [SerializeField]
    private MoveStateData moveStateData;
    [SerializeField]
    private PlayerDetectedData playerDetectedData;
    [SerializeField]
    private ChargeStateData chargeStateData;
    [SerializeField]
    private LookForPlayerStateData lookForPlayerStateData;
    [SerializeField]
    private MeleeAttackStateData meleeAttackStateData;
    [SerializeField]
    private StunStateData stunStateData;
    [SerializeField]
    private DeathStateData deathStateData;

    [SerializeField]
    private Transform meleeAttackPosition;

    public override void Start()
    {
        base.Start();
        rb.gravityScale = 1f;
        moveState = new EnemyChargeMoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new EnemyChargeIdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new EnemyChargePlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        chargeState = new EnemyChargeChargeState(this, stateMachine, "charge", chargeStateData, this);
        lookForPlayerState = new EnemyChargeLookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        meleeAttackState = new EnemyChargeMeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData,this);
        stunState = new EnemyChargeStunState(this, stateMachine, "stun", stunStateData, this);
        deathState = new EnemyChargeDeathState(this, stateMachine, "death", deathStateData, this);

        stateMachine.Initialize(moveState);
    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        //Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
    public override bool CheckPlayerInMinAgroRange()
    {
        //return base.CheckPlayerInMinAgroRange();
        return Physics2D.Raycast(playerCheck.position,Vector2.right * facingDirection, entityData.minAgroDistance, entityData.WhatIsPlayer);
    }

    public override void DamageGet(AttackDetails attackDetails)
    {
        base.DamageGet(attackDetails);
        
        if (isDeath && stateMachine.currentState != deathState) stateMachine.ChangeState(deathState);
        else if (isStunned && stateMachine.currentState != stunState) stateMachine.ChangeState(stunState);
    }
}
