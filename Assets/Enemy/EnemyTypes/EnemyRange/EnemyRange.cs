using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : Entity
{
    public EnemyRangeMoveState moveState { get; private set; }
    public EnemyRangeIdleState idleState { get; private set; }
    public EnemyRangePlayerDetectedState playerDetectedState { get; private set; }
    public EnemyRangeMeleeAttackState meleeAttackState { get; private set; }
    public EnemyRangeLookForPlayerState lookForPlayerState { get; private set; }
    public EnemyRangeStunState stunState { get; private set; }
    public EnemyRangeDodgeState dodgeState { get; private set; }
    public EnemyRangeDeathState deathstate { get; private set; }
    public EnemyRangeRangedAttackState rangedAttackState { get; private set; }

    [SerializeField]
    private MoveStateData moveStateData;
    [SerializeField]
    private IdleStateData idleStateData;
    [SerializeField]
    private PlayerDetectedData playerDetectedStateData;
    [SerializeField]
    private MeleeAttackStateData meleeAttackStateData;
    [SerializeField]
    private LookForPlayerStateData lookForPlayerStateData;
    [SerializeField]
    private StunStateData stunStateData;
    [SerializeField]
    private DeathStateData deathStateData;
    [SerializeField]
    public DodgeStateData dodgeStateData;//{ get; private set; }
    [SerializeField]
    private RangedAttackStateData rangedAttackStateData;

    [SerializeField]
    private Transform meleeAttackPosition;
    [SerializeField]
    private Transform rangedAttackPosition;
    public override void Start()
    {
        base.Start();

        moveState = new EnemyRangeMoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new EnemyRangeIdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new EnemyRangePlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData,this);
        meleeAttackState = new EnemyRangeMeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        lookForPlayerState = new EnemyRangeLookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        stunState = new EnemyRangeStunState(this, stateMachine, "stun", stunStateData, this);
        deathstate = new EnemyRangeDeathState(this, stateMachine, "death", deathStateData, this);
        dodgeState = new EnemyRangeDodgeState(this, stateMachine, "dodge", dodgeStateData, this);
        rangedAttackState = new EnemyRangeRangedAttackState(this, stateMachine, "rangedAttack", rangedAttackPosition, rangedAttackStateData, this);

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
