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
    // public FlyingRangeEnemyLookForPlayerState lookForPlayerState { get; private set; }
    // public FlyingRangeEnemyBackToPatrolState backToPatrolState { get; private set; }


    [SerializeField]
    private IdleStateData idleStateData;
    [SerializeField]
    private MoveStateData moveStateData;
    [SerializeField]
    private PlayerDetectedData playerDetectedData;
    [SerializeField]
    private RangedAttackStateData rangedAttackStateData;

    [SerializeField]
    private Transform attackPos;

    //[SerializeField]
    //private D_MeleeAttack meleeAttackStateData;
    //[SerializeField]
    //private D_LookForPlayerState lookForPlayerStateData;
    //[SerializeField]
    //private BackToPatrolStateData BackToPatrolStateData;

    public override void Start()
    {
        base.Start();
        rb.gravityScale = 0;
        moveState = new FlyingRangeEnemyMoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new FlyingRangeEnemyIdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new FlyingRangeEnemyPlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        rangedAttackState = new FlyingRangeEnemyRangedAttackState(this, stateMachine, "rangedAttack", attackPos, rangedAttackStateData, this);
        //chargeState = new FlyingEnemyChargeState(this, stateMachine, "charge", chargeStateData, this);
        //meleeAttackState = new FlyingEnemyMeleeAttackState(this, stateMachine, "meleeAttack", meleeAtackPos, meleeAttackStateData, this);
        //lookForPlayerState = new FlyingEnemyLookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        //backToPatrolState = new FlyingEnemyBackToPatrolState(this, stateMachine, "backToPatrol", BackToPatrolStateData, this);
        stateMachine.Initialize(moveState);
    }
}
