using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Entity
{
    public FE_IdleState idleState { get; private set; }
    public FE_MoveState moveState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    public override void Start()
    {
        base.Start();

        moveState = new FE_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new FE_IdleState(this, stateMachine, "idle", idleStateData, this);

        stateMachine.Initialize(moveState);
    }
}
