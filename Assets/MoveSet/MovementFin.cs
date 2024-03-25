using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFin : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] float speed = 4f;
    [SerializeField] float jumpingPower = 7f;

    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    float horizontal;

    bool isJumping, willJump;
    bool isGrounded, wasGrounded;
    bool coyoteTime;
    [SerializeField]float coyoteTimeDuration = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        GetInput();
        MoveAccordingly();

    }


    private void FixedUpdate()
    {
        // moving player horizontally:
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }


    public bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, groundLayer);
    }

    void GetInput()
    {
        bool ZPressed = false;
        horizontal = Input.GetAxisRaw("Horizontal");
        isGrounded = IsGrounded();
        if (Input.GetKeyDown(KeyCode.Z))
            ZPressed = true;

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

        if (ZPressed && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        #endregion

        wasGrounded = isGrounded;
    }

    void MoveAccordingly()
    {
        if (isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            isJumping=false;
        }
    }

    void EndCoyoteTime()
    {
        coyoteTime = false;
    }

}
