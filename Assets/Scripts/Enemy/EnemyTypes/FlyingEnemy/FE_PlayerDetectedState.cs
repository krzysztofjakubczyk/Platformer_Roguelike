public class FE_PlayerDetectedState : PlayerDetected
{
    FlyingEnemy enemy;
    public FE_PlayerDetectedState(Entity entity, BaseStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData,FlyingEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        if (entity.aliveGameObject.transform.position.x < entity.playerTransform.position.x)
        {
            if (entity.facingDirection != 1) entity.Flip();
                                                                                                    
           
        }
        else if (entity.aliveGameObject.transform.position.x > entity.playerTransform.position.x)
        {
            if (entity.facingDirection == 1)entity.Flip();
        }
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