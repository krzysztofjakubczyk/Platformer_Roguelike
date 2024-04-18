using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Entity
{
    public FE_IdleState idleState { get; private set; }
    public FE_MoveState moveState { get; private set; }
    public FE_PlayerDetectedState playerDetectedState { get; private set; }
    public FE_ChargeState chargeState { get; private set; }
    public FE_MeleeAttackState meleeAttackState {get ;private set; }
    public FE_LookForPlayerState lookForPlayerState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedData;
    [SerializeField]
    private D_ChargeState chargeStateData;
    [SerializeField]
    private D_MeleeAttack meleeAttackStateData;
    [SerializeField]
    private D_LookForPlayerState lookForPlayerStateData;

    [SerializeField]
    private Transform meleeAtackPos;
    public override void Start()
    {
        base.Start();
        rb.gravityScale = 0;
        moveState = new FE_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new FE_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new FE_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        chargeState = new FE_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        meleeAttackState = new FE_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAtackPos, meleeAttackStateData, this);
        lookForPlayerState = new FE_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        stateMachine.Initialize(moveState);
    }
}
