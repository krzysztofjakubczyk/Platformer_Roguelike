using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected D_MoveState stateData;
    [SerializeField]
    protected bool isDetectingWall;
    [SerializeField]
    protected bool isDetectingLedge;
   public MoveState(Entity entity,BaseStateMachine stateMachine,string animBoolName,D_MoveState stateData): base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        

        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        entity.SetVelocity(stateData.movementSpeed);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Debug.Log(isDetectingLedge);
        Debug.Log(isDetectingWall);
        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();
    }
}
