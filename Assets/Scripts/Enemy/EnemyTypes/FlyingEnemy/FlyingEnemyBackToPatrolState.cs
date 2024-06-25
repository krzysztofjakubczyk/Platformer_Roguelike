using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyBackToPatrolState : BackToPatrolState
{
    private FlyingEnemy enemy;
    public FlyingEnemyBackToPatrolState(Entity entity, BaseStateMachine stateMachine, string animBoolName, BackToPatrolStateData stateData, FlyingEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if (Math.Abs(Vector3.Distance(entity.aliveGameObject.transform.position, roundedPatrolPoint)) < 110f)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
        else  Debug.Log("false zmiana nietoperza na move state dystans " + Math.Abs(Vector3.Distance(entity.aliveGameObject.transform.position, roundedPatrolPoint)));
        //Debug.Log("pozycja nietoperza " + entity.aliveGameObject.transform.localPosition + "pozycja docelowa " + new Vector3((float)Math.Round(entity.patrolPoint.x, 1), (float)Math.Round(entity.patrolPoint.y, 1)) +" bool ");
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
