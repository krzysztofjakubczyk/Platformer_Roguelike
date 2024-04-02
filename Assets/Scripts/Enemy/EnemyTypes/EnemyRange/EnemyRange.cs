using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : Entity
{
    public ER_MoveState moveState { get; private set; }
    public ER_IdleState idleState { get; private set; }
    public ER_PlayerDetectedState playerDetectedState { get; private set; }
    public ER_MeleeAttackState meleeAttackState { get; private set; }
    public ER_LookForPlayerState lookForPlayerState { get; private set; }
    public ER_StunState stunState { get; private set; }

    public ER_DeathState deathstate { get; private set; }

    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedStateData;
    [SerializeField]
    private D_MeleeAttack meleeAttackStateData;
    [SerializeField]
    private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField]
    private D_StunStateData stunStateData;
    [SerializeField]
    private D_DeathState deathStateData;
    
    [SerializeField]
    private Transform meleeAttackPosition;
    public override void Start()
    {
        base.Start();

        moveState = new ER_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new ER_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new ER_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData,this);
        meleeAttackState = new ER_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        lookForPlayerState = new ER_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        stunState = new ER_StunState(this, stateMachine, "stun", stunStateData, this);
        deathstate = new ER_DeathState(this, stateMachine, "death", deathStateData, this);

        stateMachine.Initialize(moveState);
    }

    public override void DamageGet(AttackDetails attackDetails)
    {
        base.DamageGet(attackDetails);
        if (isDeath) stateMachine.ChangeState(deathstate);
        else if (isStunned && stateMachine.currentState != stunState) stateMachine.ChangeState(stunState);
        else if (!CheckPlayerInMinAgroRange())
        {
            lookForPlayerState.SetTurnImmediately(true);
            stateMachine.ChangeState(lookForPlayerState);
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}
