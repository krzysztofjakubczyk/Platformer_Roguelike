using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Entity
{
    public FE_IdleState idleState { get; private set; }
    public FE_MoveState moveState { get; private set; }
    public FE_PlayerDetectedState playerDetectedState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedData;
    public override void Start()
    {
        base.Start();
        rb.gravityScale = 0;
        moveState = new FE_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new FE_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new FE_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        stateMachine.Initialize(moveState);
    }
}
