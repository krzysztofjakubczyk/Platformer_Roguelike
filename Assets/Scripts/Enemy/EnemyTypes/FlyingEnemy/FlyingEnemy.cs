using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Entity
{
    public FlyingEnemyIdleState idleState { get; private set; }
    public FlyingEnemyMoveState moveState { get; private set; }
    public FlyingEnemyPlayerDetectedState playerDetectedState { get; private set; }
    public FlyingEnemyChargeState chargeState { get; private set; }
    public FlyingEnemyMeleeAttackState meleeAttackState {get ;private set; }
    public FlyingEnemyLookForPlayerState lookForPlayerState { get; private set; }
    public FlyingEnemyBackToPatrolState backToPatrolState { get; private set; }
    public FlyingEnemyDeathState deathState { get; private set; }
    public FlyingEnemyStunState stunState { get; private set; } 

    [SerializeField]
    private IdleStateData idleStateData;
    [SerializeField]
    private MoveStateData moveStateData;
    [SerializeField]
    private PlayerDetectedData playerDetectedData;
    [SerializeField]
    private D_ChargeState chargeStateData;
    [SerializeField]
    private MeleeAttackStateData meleeAttackStateData;
    [SerializeField]
    private LookForPlayerStateData lookForPlayerStateData;
    [SerializeField]
    private BackToPatrolStateData BackToPatrolStateData;
    [SerializeField]
    private DeathStateData deathStateData;
    [SerializeField]
    private StunStateData stunStateData;

    [SerializeField]
    private Transform meleeAtackPos;
    public override void Start()
    {
        base.Start();
        rb.gravityScale = 0;
        moveState = new FlyingEnemyMoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new FlyingEnemyIdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new FlyingEnemyPlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        chargeState = new FlyingEnemyChargeState(this, stateMachine, "charge", chargeStateData, this);
        meleeAttackState = new FlyingEnemyMeleeAttackState(this, stateMachine, "meleeAttack", meleeAtackPos, meleeAttackStateData, this);
        lookForPlayerState = new FlyingEnemyLookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        backToPatrolState = new FlyingEnemyBackToPatrolState(this, stateMachine, "backToPatrol", BackToPatrolStateData, this);
        deathState = new FlyingEnemyDeathState(this, stateMachine, "death", deathStateData, this);
        stunState = new FlyingEnemyStunState(this, stateMachine, "stun", stunStateData, this);
        stateMachine.Initialize(moveState);
    }
    public override void DamageGet(AttackDetails attackDetails)
    {
        base.DamageGet(attackDetails);
        if (isDeath) stateMachine.ChangeState(deathState);
        else if (isStunned && stateMachine.currentState != stunState) stateMachine.ChangeState(stunState);
        else if (CheckPlayerInMinAgroRange()) stateMachine.ChangeState(chargeState);
        else if (!CheckPlayerInMinAgroRange())
        {
            lookForPlayerState.SetTurnImmediately(true);
            stateMachine.ChangeState(lookForPlayerState);
        }
    }
}
