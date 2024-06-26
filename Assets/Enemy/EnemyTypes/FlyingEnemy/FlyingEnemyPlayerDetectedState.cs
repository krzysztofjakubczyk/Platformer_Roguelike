public class FlyingEnemyPlayerDetectedState : PlayerDetected
{
    FlyingEnemy enemy;
    public FlyingEnemyPlayerDetectedState(Entity entity, BaseStateMachine stateMachine, string animBoolName, PlayerDetectedData stateData,FlyingEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
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
        entity.CheckFlipToPlayer();
        if (performCloseRangeAction) stateMachine.ChangeState(enemy.meleeAttackState);
        else if (performLongRangeAction) stateMachine.ChangeState(enemy.chargeState);
        else if (!isPlayerInMaxAgroRange)
        {
             stateMachine.ChangeState(enemy.lookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}