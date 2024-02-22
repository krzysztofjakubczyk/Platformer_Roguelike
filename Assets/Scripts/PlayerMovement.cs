using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] float speed = 8f;
    [SerializeField] float jumpingPower = 16f;

    float horizontal;
    float coyoteTimeDuration = 0.1f;
    bool isFacingRight = true;
    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    bool willJump;
    bool wasGrounded, coyoteTime;
    bool isJumping, canCheckJumping;
    [SerializeField] float upRaycastHight = 1.5f;

    bool razStop = false;

    //[SerializeField] float acceleration = 70;
    //[SerializeField] float deacceleration = 50;
    [SerializeField] float currentSpeed = 0;
    [SerializeField] float currentForwardDirection = 1;
    [SerializeField] float fullTimeGo = 1;
    [SerializeField] float fullTimeStop = 1;

    float targetSpeed;
    float timer1;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        //prevFrameDir = horizontal;
        GetInput();
        Flip();

        if ((currentForwardDirection == -1 && horizontal == 1 || currentForwardDirection == 1 && horizontal == -1) && !razStop)
        {
            timer1 = 0;
            razStop = true;
        }

        //if (horizontal == 1)
        //    currentForwardDirection = 1;
        //else if(horizontal == -1)
        //    currentForwardDirection = -1;

        if (horizontal != 0)
        {
            if (timer1 < 1)
                timer1 += Time.fixedDeltaTime * fullTimeGo;
            else
            {
                currentForwardDirection = horizontal;
                razStop = false;
            }

            currentSpeed = 1 - (1 - timer1) * (1 - timer1); //Mathf.Sqrt(timer1);// * timer1;

            currentSpeed = Mathf.Clamp(currentSpeed * 8, 0, speed);

            rb.velocity = new Vector2(currentSpeed * horizontal, rb.velocity.y);
        }
        else
        {
            //timer1 = 0;
            //rb.velocity = new Vector2(0, rb.velocity.y);
            if (timer1 > 0)
                timer1 -= Time.fixedDeltaTime * fullTimeStop;
            else
                currentForwardDirection = 0;

            currentSpeed = 1 - (1 - timer1) * (1 - timer1); //Mathf.Sqrt(timer1);// * timer1;

            currentSpeed = Mathf.Clamp(currentSpeed * 8, 0, speed);

            rb.velocity = new Vector2(currentSpeed * currentForwardDirection, rb.velocity.y);
        }
    }


    private void FixedUpdate()
    {

        

    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, groundLayer);
    }

    void GetInput()
    {
        bool isGrounded = IsGrounded();

        // get input
        horizontal = Input.GetAxisRaw("Horizontal");
        //if (!isGrounded)
           // horizontal /= 1.3f;

        if (isGrounded)
            isJumping = false;

        // if player just stopped being grounded and didnt jump - activate coyote time
        if (wasGrounded && !isJumping)
        {
            if (!isGrounded)
            {
                coyoteTime = true;
                Invoke(nameof(endCoyoteTime), coyoteTimeDuration);
            }
        }

        // check if player is blocked by edge while jumping
        if (isJumping)
        {
            noPixelBlocking();
        }

        // eliminating pixel perfect jumps
        if (Input.GetKeyDown(KeyCode.Z) && !isGrounded)
        {
            if (Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .5f, groundLayer))
            {
                willJump = true;
            }
        }

        // jump
        if (  (Input.GetKeyDown(KeyCode.Z) || willJump)  &&  (isGrounded || coyoteTime) )
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            isJumping = true;
            willJump = false;
        }

        // lower jump velocity when jump key up
        if (Input.GetKeyUp(KeyCode.Z) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        // save isGrounded from previous frame
        if (isGrounded) wasGrounded = true;
        else wasGrounded = false;
    }

    void endCoyoteTime()
    {
        coyoteTime = false;
    }

    void noPixelBlocking()
    {
        bool left = Physics2D.Raycast(GetComponent<BoxCollider2D>().bounds.center + new Vector3(0.2f, 0, 0), Vector2.up, upRaycastHight, groundLayer);
        bool right = Physics2D.Raycast(GetComponent<BoxCollider2D>().bounds.center + new Vector3(-0.2f, 0, 0), Vector2.up, upRaycastHight, groundLayer);
        bool leftBound = Physics2D.Raycast(GetComponent<BoxCollider2D>().bounds.center + new Vector3(-GetComponent<BoxCollider2D>().size.x / 2, 0, 0), Vector2.up, upRaycastHight, groundLayer);
        bool rightBound = Physics2D.Raycast(GetComponent<BoxCollider2D>().bounds.center + new Vector3(GetComponent<BoxCollider2D>().size.x / 2, 0, 0), Vector2.up, upRaycastHight, groundLayer);

        if (horizontal == 0)
        {
            if (rightBound && !left)
                transform.position += new Vector3(-0.3f, 0, 0);
            else if (leftBound && !right)
                transform.position += new Vector3(0.3f, 0, 0);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(GetComponent<BoxCollider2D>().bounds.center + new Vector3(0.2f, 0, 0), Vector2.up * 1.5f);
        Gizmos.DrawRay(GetComponent<BoxCollider2D>().bounds.center + new Vector3(-0.2f, 0, 0), Vector2.up * 1.5f);
        Gizmos.DrawRay(GetComponent<BoxCollider2D>().bounds.center + new Vector3(-GetComponent<BoxCollider2D>().size.x / 2, 0, 0), Vector2.up * 1.5f);
        Gizmos.DrawRay(GetComponent<BoxCollider2D>().bounds.center + new Vector3(GetComponent<BoxCollider2D>().size.x / 2, 0, 0), Vector2.up * 1.5f);
    }
}
