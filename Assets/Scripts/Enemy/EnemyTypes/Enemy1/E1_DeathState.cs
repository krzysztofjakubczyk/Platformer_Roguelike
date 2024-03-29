using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_DeathState : DeathState
{
    private Enemy1 enemy;

    public E1_DeathState(Entity entity, BaseStateMachine stateMachine, string animBoolName, D_DeathState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
}
