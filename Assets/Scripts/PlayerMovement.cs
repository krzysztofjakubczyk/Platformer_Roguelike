using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer; // posprzatac input funkcje, dodac raycasty do nie blokowania skoku przez fragment collidera
    [SerializeField] float speed = 8f;
    [SerializeField] float jumpingPower = 16f;

    float horizontal;
    bool isFacingRight = true;
    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    bool willJump;
    bool wasGrounded, coyoteTime;
    bool isJumping, canCheckJumping;
    [SerializeField] float upRaycastHight = 1.5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        GetInput();
        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, groundLayer);
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

    void GetInput()
    {

        if (wasGrounded && !isJumping)
        {
            if (!IsGrounded())
            {
                coyoteTime = true;
                Invoke(nameof(endCoyoteTime), 0.1f);

            }
        }

        if (isJumping) // spr czy nic go nie blokuje co nie powinno
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

        if (isJumping && IsGrounded() && canCheckJumping)
        {
            isJumping = false;
            canCheckJumping = false;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        if (!IsGrounded())
            horizontal /= 1.3f;



        if (willJump && IsGrounded() && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            isJumping = true;
            willJump = false;
            Invoke(nameof(NotJupming), 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.Z) && !IsGrounded())
        {
            if (Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .5f, groundLayer))
            {
                willJump = true;
            }
        }


        if (Input.GetKeyDown(KeyCode.Z) && (IsGrounded() || coyoteTime) && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            isJumping = true;
            Invoke(nameof(NotJupming), 0.5f);
        }


        if (Input.GetKeyUp(KeyCode.Z) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (IsGrounded())
            wasGrounded = true;
        else
            wasGrounded = false;
    }

    void endCoyoteTime()
    {
        coyoteTime = false;
    }

    void NotJupming()
    {
        canCheckJumping = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(GetComponent<BoxCollider2D>().bounds.center + new Vector3(0.2f, 0, 0), Vector2.up * 1.5f);
        Gizmos.DrawRay(GetComponent<BoxCollider2D>().bounds.center + new Vector3(-0.2f, 0, 0), Vector2.up * 1.5f);
        Gizmos.DrawRay(GetComponent<BoxCollider2D>().bounds.center + new Vector3(-GetComponent<BoxCollider2D>().size.x / 2, 0, 0), Vector2.up * 1.5f);
        Gizmos.DrawRay(GetComponent<BoxCollider2D>().bounds.center + new Vector3(GetComponent<BoxCollider2D>().size.x / 2, 0, 0), Vector2.up * 1.5f);


    }
}
