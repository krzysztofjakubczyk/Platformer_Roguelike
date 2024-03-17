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
    private Vector2 velocityWorkspace;

    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform LedgeCheck;
    public virtual void Start()
    {
        facingDirection = 1;
        aliveGameObject = transform.Find("Alive").gameObject; //znalezienie ¿ywego przeciwnika ale co jak bêdzie ich wiêcej, czyli do zmiany, wiêc zabieg Danielowy czyli  tymczasowy
        rb = aliveGameObject.GetComponent<Rigidbody2D>();
        anim = aliveGameObject.GetComponent<Animator>();

        stateMachine = new BaseStateMachine();
       
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
        Debug.Log(stateMachine.currentState);
        Debug.Log(rb.velocity);
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
    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGameObject.transform.right, entityData.wallCheckDistance, entityData.WhatIsGround);
    }
    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(LedgeCheck.position, Vector2.down, entityData.LedgeCheckDistance, entityData.WhatIsGround);
    }
    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGameObject.transform.Rotate(0f,180f,0f);
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
        Gizmos.DrawLine(LedgeCheck.position, LedgeCheck.position + (Vector3)(Vector2.down* entityData.LedgeCheckDistance));
    }
}
