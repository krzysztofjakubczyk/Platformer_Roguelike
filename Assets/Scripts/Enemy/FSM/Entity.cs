using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public D_Entity entityData;

    public BaseStateMachine stateMachine;
    public int facingDirection { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public GameObject aliveGameObject { get; private set; }
    public AnimationToStateMachine atsm { get; private set; }
    public int lastDamageDirection { get; private set; }
    public Transform playerTransform { get; private set; }
    [SerializeField] private GameObject player;
    public HealthController playerHp;
    public float currentHealth;
    public float currentStunResistance;
    public float lastDamageTime;

    private Vector2 velocityWorkspace;

    protected bool isStunned;
    protected bool isDeath;

    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform LedgeCheck;
    [SerializeField] public Transform playerCheck;
    [SerializeField] private Transform groundCheck;
    
    public virtual void Start()
    {
        facingDirection = 1;
        currentHealth = entityData.maxHealth;
        currentStunResistance = entityData.stunResistance;

        playerHp = player.GetComponent<HealthController>();
        aliveGameObject = transform.Find("Alive").gameObject; //znalezienie ¿ywego przeciwnika ale co jak bêdzie ich wiêcej, czyli do zmiany, wiêc zabieg Danielowy czyli  tymczasowy
        rb = aliveGameObject.GetComponent<Rigidbody2D>();
        anim = aliveGameObject.GetComponent<Animator>();
        atsm = aliveGameObject.GetComponent<AnimationToStateMachine>();

        stateMachine = new BaseStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();

        //anim.SetFloat("yVelocity", rb.velocity.y); do animacji dodgowania w góre albo w dó³, tymczasowo puste

        if(Time.time >=lastDamageTime + entityData.stunRecoveryTime)
        {
            ResetStunResistance();
        }

    }
    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }
    public virtual void StateMachine()
    {
        stateMachine.currentState.PhysicsUpdate();
    }
    public virtual void SetVelocity(float velocity)
    {
        velocityWorkspace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = velocityWorkspace;
    }
    public virtual void SetVelocity(float velocity,Vector2 angle, int direction)
    {
        angle.Normalize();
        velocityWorkspace.Set(angle.x * velocity * direction, angle.y * velocity);
        rb.velocity = velocityWorkspace;
    }
    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGameObject.transform.right, entityData.wallCheckDistance, entityData.WhatIsGround);
    }
    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(LedgeCheck.position, Vector2.down, entityData.LedgeCheckDistance, entityData.WhatIsGround);
    }
    public virtual bool CheckGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, entityData.groundCheckRadius, entityData.WhatIsGround);
    }
    public virtual void DamageHop(float velocity)
    {
        velocityWorkspace.Set(rb.velocity.x, velocity);
        rb.velocity = velocityWorkspace;
    }
    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }
    public virtual void DamageGet(AttackDetails attackDetails)
    {
        lastDamageTime = Time.time;

        currentHealth -= attackDetails.damageAmount;
        currentStunResistance -= attackDetails.stunDamageAmount;

        DamageHop(entityData.damageHopSpeed);

        Instantiate(entityData.hitParticle, aliveGameObject.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
        if(attackDetails.position.x > aliveGameObject.transform.position.x)
        {
            lastDamageDirection = -1;
        }
        else
        {
            lastDamageDirection = 1;
        }
        if (currentStunResistance <= 0) isStunned = true;

        if (currentHealth <= 0) isDeath = true;
    }
    public virtual void Flip()
    {
        
        facingDirection *= -1;
        aliveGameObject.transform.Rotate(0f,180f,0f);
    }
    public virtual bool CheckPlayerInMinAgroRange()
    {
        RaycastHit2D hitGround = Physics2D.Raycast(playerCheck.position, aliveGameObject.transform.right,entityData.minAgroDistance, entityData.WhatIsGround);
        Collider2D hitPlayer = Physics2D.OverlapCircle(playerCheck.position, entityData.minAgroDistance, entityData.WhatIsPlayer);
        if(hitPlayer) playerTransform = hitPlayer.transform;
        if (hitGround && hitPlayer && hitGround.distance < Mathf.Abs(transform.position.x - hitPlayer.transform.position.x)) return false;
        else return Physics2D.OverlapCircle(playerCheck.position,  entityData.minAgroDistance, entityData.WhatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.OverlapCircle(playerCheck.position, entityData.maxAgroDistance, entityData.WhatIsPlayer);
    }
    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.OverlapCircle(playerCheck.position, entityData.closeRangeActionDistance, entityData.WhatIsPlayer);
    }
    public virtual bool CheckEnemyInRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGameObject.transform.right, entityData.minAgroDistance, entityData.WhatIsEnemy);
    }
    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
        Gizmos.DrawLine(LedgeCheck.position, LedgeCheck.position + (Vector3)(Vector2.down* entityData.LedgeCheckDistance));

        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.closeRangeActionDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.minAgroDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.maxAgroDistance), 0.2f);
    }
    
}
