using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    DeathStateData stateData;
    public DeathState(Entity entity, BaseStateMachine stateMachine, string animBoolName, DeathStateData stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        GameObject.Instantiate(stateData.deathBloodParticle, entity.aliveGameObject.transform.position, stateData.deathBloodParticle.transform.rotation);
        GameObject.Instantiate(stateData.deathChunkParticle, entity.aliveGameObject.transform.position, stateData.deathChunkParticle.transform.rotation);

        entity.gameObject.SetActive(false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    //private IEnumerator waitAfterDeath()
    //{
    //    yield return new WaitForSeconds(5);
    //    entity.gameObject.SetActive(false);
    //}
}
