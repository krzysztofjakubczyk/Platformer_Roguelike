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
        stateMachine.Initialize(moveState);
    }
}
