using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingRangeEnemyBackToPatrolState : BackToPatrolState
{
    private FlyingRangeEnemy enemy;

    public FlyingRangeEnemyBackToPatrolState(Entity entity, BaseStateMachine stateMachine, string animBoolName, BackToPatrolStateData stateData, FlyingRangeEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Vector3 roundedPatrolPoint = new Vector3(
  (float)Math.Round(entity.patrolPoint.x, 1),
  (float)Math.Round(entity.patrolPoint.y, 1),
  entity.aliveGameObject.transform.position.z
);

        if (Math.Abs(Vector3.Distance(entity.aliveGameObject.transform.localPosition, roundedPatrolPoint)) < 1f)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
        else Debug.Log("false zmiana diab³a na move state dystans " 
            + Math.Abs(Vector3.Distance(entity.aliveGameObject.transform.localPosition, roundedPatrolPoint))
            + "pozycja globalna "+entity.aliveGameObject.transform.position+
            "pozycja lokalna "+ entity.aliveGameObject.transform.localPosition);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
