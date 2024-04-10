using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFin : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask enemyLayer;
    LayerMask pustwarstwa;
    [SerializeField] float speed = 4f;
    [SerializeField] float jumpingPower = 7f;
    [SerializeField] float dashPower = 5f;
    [SerializeField] float leftWallJump = 0.5f;
    [SerializeField] float rightWallJump = 0.5f;

    Rigidbody2D rb;
    BoxCollider2D boxCollider;

    //wall jumps colliders
    [SerializeField] GameObject WJleft;
    [SerializeField] GameObject WJright;
    float horizontal;
    float lastDirection;

    bool isJumping, willJump;
    bool isGrounded, wasGrounded;
    bool coyoteTime;
    [SerializeField]float coyoteTimeDuration = 1;
    bool isDashing;
    bool sideJump;

    bool isJumpingRight, isJumpingLeft;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        Application.targetFrameRate = 120;
    }

    void Update()
    {
        GetInput();
        MoveAccordingly();
    }


    private void FixedUpdate()
    {
        // moving player horizontally:
        if(!isDashing && !sideJump)
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (isJumpingLeft)
        {
            rb.velocity = new Vector2(-jumpingPower, jumpingPower);
            isJumpingLeft = false;
            Invoke(nameof(EndSideJump), 0.1f);
        }
        else if (isJumpingRight)
        {
            rb.velocity = new Vector2(jumpingPower, jumpingPower);
            isJumpingRight = false;
            Invoke(nameof(EndSideJump), 0.1f);
        }
    }


    public bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, groundLayer);
    }

    void GetInput()
    {
        bool ZPressed = false;
        horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal != 0)
            lastDirection = horizontal;
        isGrounded = IsGrounded();
        if (Input.GetKeyDown(KeyCode.Z))
            ZPressed = true;

        if (Input.GetKeyDown(KeyCode.X))
        {

            isDashing = true;
        }

        // if player run out of platform (but didnt jump) activate coyoteTime
        if (!isGrounded && wasGrounded && !Input.GetKey(KeyCode.Z))
        {
            coyoteTime = true;
            Invoke(nameof(EndCoyoteTime), coyoteTimeDuration);
        }

        #region JUMP

        // jump works even when player is 0.5 above the ground to eliminate need for pixel perfect jumps
        if (ZPressed && !isGrounded && !coyoteTime)
        {
            if (Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.5f, groundLayer))
            {
                willJump = true;
            }
        }
        else if (ZPressed && (isGrounded || coyoteTime))
        {
            isJumping = true;
            coyoteTime = false;
        }

        if (willJump && isGrounded)
        {
            isJumping = true;
            willJump = false;
        }

        if (Input.GetKeyUp(KeyCode.Z) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        #endregion

        //wall jump
        //if (Physics2D.Raycast(transform.position, Vector2.right, leftWallJump, groundLayer) && !isGrounded && ZPressed)
        if (WJleft.GetComponent<ifboxtriggered>().isTriggered && !isGrounded && ZPressed)
        {
            isJumpingLeft = true;
            sideJump = true;
        }


        if (WJright.GetComponent<ifboxtriggered>().isTriggered && !isGrounded && ZPressed)
        {
            isJumpingRight = true;
            sideJump = true;
        }

        wasGrounded = isGrounded;
    }

    void MoveAccordingly()
    {
        if (isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            isJumping=false;
        }


        if (isDashing)
        {
            rb.AddForce(new Vector2(lastDirection * dashPower, 0),ForceMode2D.Impulse);
            boxCollider.excludeLayers = enemyLayer;
            Invoke(nameof(EndDash), 0.1f);
            print(lastDirection);
        }
    }

    void EndCoyoteTime()
    {
        coyoteTime = false;
    }

    void EndDash()
    {
        isDashing = false;
        boxCollider.excludeLayers = pustwarstwa;
    }

    void EndSideJump()
    {
        sideJump = false;
    }
}
