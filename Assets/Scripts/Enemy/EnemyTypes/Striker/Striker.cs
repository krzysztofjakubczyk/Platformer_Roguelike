using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Striker : Entity
{
    public StrikerIdleState idleState { get; private set; }
    public StrikerMoveState moveState { get; private set; }

    [SerializeField]
    private IdleStateData idleStateData;
    [SerializeField]
    private MoveStateData moveStateData;

    public override void Start()
    {
        base.Start();
        rb.gravityScale = 1f;
        moveState = new StrikerMoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new StrikerIdleState(this, stateMachine, "idle", idleStateData, this);
        stateMachine.Initialize(moveState);
    }
    }
