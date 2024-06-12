using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Striker : Entity
{
    public StrikerIdleState idleState { get; private set; }
    public StrikerMoveState moveState { get; private set; }
    public StrikerStunState stunState { get; private set; }
    public StrikerDeathState deathState { get; private set; }
    public StrikerPlayerDetectedState playerDetectedState { get; private set; }
    public StrikerLookForPlayerState lookForPlayerState { get; private set; }
    public StrikerMeleeAttackState meleeAttackState { get; private set; }

    [SerializeField]
    private IdleStateData idleStateData;
    [SerializeField]
    private MoveStateData moveStateData;
    [SerializeField]
    private StunStateData stunStateData;
    [SerializeField]
    private DeathStateData deathStateData;
    [SerializeField]
    private PlayerDetectedData playerDetectedData;
    [SerializeField]
    private LookForPlayerStateData lookForPlayerStateData;
    [SerializeField]
    private MeleeAttackStateData meleeAttackStateData;

    [SerializeField]
    private Transform meleeAttackPosition;

    public override void Start()
    {
        base.Start();
        rb.gravityScale = 1f;
        moveState = new StrikerMoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new StrikerIdleState(this, stateMachine, "idle", idleStateData, this);
        stunState = new StrikerStunState(this, stateMachine, "stun", stunStateData, this);
        deathState = new StrikerDeathState(this, stateMachine, "death", deathStateData, this);
        playerDetectedState = new StrikerPlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        lookForPlayerState = new StrikerLookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        meleeAttackState = new StrikerMeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        stateMachine.Initialize(moveState);
    }

    public override void DamageGet(AttackDetails attackDetails)
    {
        base.DamageGet(attackDetails);
        if (isDeath && stateMachine.currentState != deathState) stateMachine.ChangeState(deathState);
        else if (isStunned && stateMachine.currentState != stunState) stateMachine.ChangeState(stunState);
    }

}
