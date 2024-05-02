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
    public ER_DodgeState dodgeState { get; private set; }
    public ER_DeathState deathstate { get; private set; }
    public ER_RangedAttackState rangedAttackState { get; private set; }

    [SerializeField]
    private MoveStateData moveStateData;
    [SerializeField]
    private IdleStateData idleStateData;
    [SerializeField]
    private PlayerDetectedData playerDetectedStateData;
    [SerializeField]
    private D_MeleeAttack meleeAttackStateData;
    [SerializeField]
    private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField]
    private D_StunStateData stunStateData;
    [SerializeField]
    private D_DeathState deathStateData;
    [SerializeField]
    public D_DodgeState dodgeStateData;//{ get; private set; }
    [SerializeField]
    private RangedAttackStateData rangedAttackStateData;

    [SerializeField]
    private Transform meleeAttackPosition;
    [SerializeField]
    private Transform rangedAttackPosition;
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
        dodgeState = new ER_DodgeState(this, stateMachine, "dodge", dodgeStateData, this);
        rangedAttackState = new ER_RangedAttackState(this, stateMachine, "rangedAttack", rangedAttackPosition, rangedAttackStateData, this);

        stateMachine.Initialize(moveState);
    }

    public override void DamageGet(AttackDetails attackDetails)
    {
        base.DamageGet(attackDetails);
        if (isDeath) stateMachine.ChangeState(deathstate);
        else if (isStunned && stateMachine.currentState != stunState) stateMachine.ChangeState(stunState);
        else if (CheckPlayerInMinAgroRange()) stateMachine.ChangeState(rangedAttackState);
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
