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
    Animator animator;
    Transform sword;

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

    KeyCode jumpKey = KeyCode.Z;
    KeyCode dashKey = KeyCode.LeftShift;
    //KeyCode spellKey = KeyCode.X;
    //KeyCode meleeKey = KeyCode.C;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        sword = transform.GetChild(0);

        Application.targetFrameRate = 120;
    }

    void Update()
    {
        GetInput();
        MoveAccordingly();

        if (Input.GetKey(KeyCode.O)){
            animator.SetTrigger("upTest");
        }
    }


    private void FixedUpdate()
    {
        // moving player horizontally:
        if(!isDashing && !sideJump)
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (Mathf.Abs(horizontal * speed) > 0.1f)
            animator.SetBool("isRunning", true);
        else
        {
            animator.SetBool("isRunning", false);
            //print(horizontal * speed);
        }

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
        animator.SetBool("isGrounded", isGrounded);

        if (Input.GetKeyDown(jumpKey))
            ZPressed = true;

        if (Input.GetKeyDown(dashKey))
            isDashing = true;


        // if player run out of platform (but didnt jump) activate coyoteTime
        if (!isGrounded && wasGrounded && !Input.GetKey(jumpKey))
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
        if(horizontal > 0)
            transform.GetComponent<SpriteRenderer>().flipX = true;
        else if (horizontal < 0)
            transform.GetComponent<SpriteRenderer>().flipX = false;

        if(horizontal == 0 && rb.velocity.x > 0.1f)
            transform.GetComponent<SpriteRenderer>().flipX = true;
        else if (horizontal == 0 && rb.velocity.x < -0.1f)
            transform.GetComponent<SpriteRenderer>().flipX = false;


        if (isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            isJumping=false;
        }


        if (isDashing)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(lastDirection * dashPower, 0),ForceMode2D.Impulse);
            boxCollider.excludeLayers = enemyLayer;
            Invoke(nameof(EndDash), 0.1f);
            Invoke(nameof(EndDashArmor), 0.5f);
        }
    }

    void EndCoyoteTime()
    {
        coyoteTime = false;
    }

    void EndDash()
    {
        isDashing = false;
    }

    void EndDashArmor()
    {
        boxCollider.excludeLayers = pustwarstwa;
    }

    void EndSideJump()
    {
        sideJump = false;
    }
    public float changeSpeed(float howMany) 
    {
        speed += howMany;
        return speed;
    }

    public float ChangeJump(float jump) 
    { 
        jumpingPower += jump;
        return jumpingPower;
    }

    public float ChangeDash(float dash) 
    { 
        dashPower += dash;
        return dashPower;
    }

    public float GetJump() { return jumpingPower; }

    public float GetDash() { return dashPower; }

    public float GetMovement() { return speed; }
}
