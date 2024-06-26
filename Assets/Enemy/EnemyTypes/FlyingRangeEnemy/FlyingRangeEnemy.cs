using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingRangeEnemy : Entity
{
    public FlyingRangeEnemyIdleState idleState { get; private set; }
    public FlyingRangeEnemyMoveState moveState { get; private set; }
    public FlyingRangeEnemyPlayerDetectedState playerDetectedState { get; private set; }
    public FlyingRangeEnemyRangedAttackState rangedAttackState { get; private set; }
    // public FlyingRangeEnemyChargeState chargeState { get; private set; }
    // public FlyingRangeEnemyMeleeAttackState meleeAttackState { get; private set; }
    public FlyingRangeEnemyLookForPlayerState lookForPlayerState { get; private set; }
    public FlyingRangeEnemyStunState stunState { get; private set; }
    public FlyingRangeEnemyDeathState deathState { get; private set; }

     public FlyingRangeEnemyBackToPatrolState backToPatrolState { get; private set; }


    [SerializeField]
    private IdleStateData idleStateData;
    [SerializeField]
    private MoveStateData moveStateData;
    [SerializeField]
    private PlayerDetectedData playerDetectedData;
    [SerializeField]
    private RangedAttackStateData rangedAttackStateData;
    [SerializeField]
    private StunStateData stunStateData;
    [SerializeField]
    private DeathStateData deathStateData;

    [SerializeField]
    private Transform attackPos;

    //[SerializeField]
    //private D_MeleeAttack meleeAttackStateData;
    [SerializeField]
    private LookForPlayerStateData lookForPlayerStateData;
    [SerializeField]
    private BackToPatrolStateData BackToPatrolStateData;

    public override void Start()
    {
        base.Start();
        rb.gravityScale = 0;
        moveState = new FlyingRangeEnemyMoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new FlyingRangeEnemyIdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new FlyingRangeEnemyPlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        rangedAttackState = new FlyingRangeEnemyRangedAttackState(this, stateMachine, "rangedAttack", attackPos, rangedAttackStateData, this);
        stunState = new FlyingRangeEnemyStunState(this, stateMachine, "stun", stunStateData, this);
        deathState = new FlyingRangeEnemyDeathState(this, stateMachine, "death", deathStateData, this);
        //chargeState = new FlyingEnemyChargeState(this, stateMachine, "charge", chargeStateData, this);
        //meleeAttackState = new FlyingEnemyMeleeAttackState(this, stateMachine, "meleeAttack", meleeAtackPos, meleeAttackStateData, this);
        lookForPlayerState = new FlyingRangeEnemyLookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        backToPatrolState = new FlyingRangeEnemyBackToPatrolState(this, stateMachine, "backToPatrol", BackToPatrolStateData, this);
        stateMachine.Initialize(moveState);
    }
    public override void DamageGet(AttackDetails attackDetails)
    {
        base.DamageGet(attackDetails);
        if (isDeath) stateMachine.ChangeState(deathState);
        else if (isStunned && stateMachine.currentState != stunState) stateMachine.ChangeState(stunState);
        else if (CheckPlayerInMinAgroRange()) stateMachine.ChangeState(rangedAttackState);
        else if (!CheckPlayerInMinAgroRange())
        {
            lookForPlayerState.SetTurnImmediately(true);
            stateMachine.ChangeState(lookForPlayerState);
        }
    }
}
