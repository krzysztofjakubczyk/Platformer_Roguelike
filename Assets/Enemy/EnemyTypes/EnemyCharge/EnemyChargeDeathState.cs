using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChargeDeathState : DeathState
{
    private EnemyCharge enemy;

    public EnemyChargeDeathState(Entity entity, BaseStateMachine stateMachine, string animBoolName, DeathStateData stateData, EnemyCharge enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
}
