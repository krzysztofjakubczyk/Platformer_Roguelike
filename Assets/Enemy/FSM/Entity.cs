using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public EntityData entityData;
    [SerializeField]
    private GameObject sceneManager;
    
    public AudioSource audioSource;
    
    private SceneController sceneController;
    
    public BaseStateMachine stateMachine;
    public int facingDirection { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public GameObject aliveGameObject { get; private set; }
    public AnimationToStateMachine atsm { get; private set; }
    public int lastDamageDirection { get; private set; }
    public Transform playerTransform { get; private set; }
    [SerializeField] private GameObject player;

    [SerializeField] public Vector2 patrolPoint;

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

    public static event Action OnEnemyDeath;
    public virtual void Start()
    {
        sceneController = sceneManager.GetComponent<SceneController>();
        //sceneController.changeEnemyPos += SetPatrolPoint;
        facingDirection = 1;
        currentHealth = entityData.maxHealth;
        currentStunResistance = entityData.stunResistance;
        player = GameObject.FindGameObjectWithTag("Player");
        playerHp = player.GetComponent<HealthController>();
        Debug.Log(playerHp + " Player hp");
        aliveGameObject = transform.Find("Alive").gameObject; //znalezienie ¿ywego przeciwnika ale co jak bêdzie ich wiêcej, czyli do zmiany, wiêc zabieg Danielowy czyli  tymczasowy
        rb = aliveGameObject.GetComponent<Rigidbody2D>();
        anim = aliveGameObject.GetComponent<Animator>();
        atsm = aliveGameObject.GetComponent<AnimationToStateMachine>();
        audioSource = aliveGameObject.GetComponent<AudioSource>();
        stateMachine = new BaseStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
        playerTransform = player.transform;
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

        Instantiate(entityData.hitParticle, aliveGameObject.transform.position, Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0f, 360f)));
        if(attackDetails.position.x > aliveGameObject.transform.position.x)
        {
            lastDamageDirection = -1;
        }
        else
        {
            lastDamageDirection = 1;
        }
        if (currentStunResistance <= 0) isStunned = true;

        if (currentHealth <= 0)
        {
            isDeath = true;
            
            OnEnemyDeath?.Invoke();
            GameObject.Destroy(gameObject,0.1f);
        }
    }
    public virtual void Flip()
    {
        
        facingDirection *= -1;
        aliveGameObject.transform.Rotate(0f,180f,0f);
    }
    public virtual bool CheckPlayerInMinAgroRange()
    {
        RaycastHit2D hitGround = Physics2D.Raycast(playerCheck.position, aliveGameObject.transform.right, entityData.minAgroDistance, entityData.WhatIsGround);
        Collider2D hitPlayer = Physics2D.OverlapCircle(playerCheck.position, entityData.minAgroDistance, entityData.WhatIsPlayer);
        if (hitPlayer)
        {
            playerTransform = hitPlayer.transform;
            hitGround = Physics2D.Raycast(playerCheck.position, playerTransform.position - aliveGameObject.transform.position, entityData.minAgroDistance, entityData.WhatIsGround);
        }
        if (hitGround && hitPlayer && hitGround.distance < Mathf.Abs((aliveGameObject.transform.position - playerTransform.position).magnitude)) return false;
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
        return Physics2D.Raycast(playerCheck.position, aliveGameObject.transform.right, entityData.minEnemyAgroDistance, entityData.WhatIsEnemy);
    }
    public virtual bool CheckEnemyInRangeToCharge()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGameObject.transform.right, entityData.minAgroDistance, entityData.WhatIsEnemy);
    }
    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
        Gizmos.DrawLine(LedgeCheck.position, LedgeCheck.position + (Vector3)(Vector2.down* entityData.LedgeCheckDistance));

        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.closeRangeActionDistance), 0.2f);
        if(playerTransform)Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(aliveGameObject.transform.position - playerTransform.position * entityData.minAgroDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.maxAgroDistance), 0.2f);
       if(playerTransform) Gizmos.DrawLine(playerCheck.position, playerCheck.position + playerTransform.position - aliveGameObject.transform.position);
        Gizmos.DrawLine(playerCheck.position, playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.minAgroDistance));
    }
    public virtual void CheckFlipToPlayer()
    {
        if (aliveGameObject.transform.position.x < playerTransform.position.x)
        {
            if (facingDirection != 1) Flip();
        }
        else if (aliveGameObject.transform.position.x > playerTransform.position.x)
        {
            if (facingDirection == 1) Flip();
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == entityData.WhatIsPlayer)
        {
            //wy³¹czyæ movemnt gracza
            Debug.Log("odbija");
            rb = collision.transform.GetComponent<Rigidbody2D>();
            if(collision.transform.position.x < transform.position.x)
            rb.AddForce(new Vector2(-1, -1), ForceMode2D.Impulse);
            else if (collision.transform.position.x > transform.position.x)
                rb.AddForce(new Vector2(1, 1), ForceMode2D.Impulse);
        }
    }
    //private void SetPatrolPoint()
    //{
    //    patrolPoint = transform.position;
    //    print(patrolPoint + " pozycja patrolowa "+ aliveGameObject.transform.parent.gameObject.name);
    //}

}
