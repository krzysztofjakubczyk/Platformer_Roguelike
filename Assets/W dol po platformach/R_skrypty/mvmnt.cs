using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mvmnt : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] float speed = 8f;
    [SerializeField] float jumpingPower = 16f;

    float horizontal;
    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    bool coyoteTime;
    bool isJumping, willJump;
    bool numbState;
    [SerializeField] GameObject imageRed;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        numbState = false;
    }

    void Update()
    {
        if(!numbState)
            GetInput(); 
    }

    private void FixedUpdate()
    {
        //rb.AddForce(new Vector2(horizontal * speed, 0), ForceMode2D.Force);
        if(!numbState)
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    public bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, groundLayer);
    }

    void GetInput()
    {
        bool isGrounded = IsGrounded();

        // get input
        horizontal = Input.GetAxisRaw("Horizontal");

        if (isGrounded)
            isJumping = false;


        // eliminating pixel perfect jumps
        if (Input.GetKeyDown(KeyCode.Z) && !isGrounded)
        {
            if (Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .5f, groundLayer))
            {
                willJump = true;
            }
        }

        // jump
        if ((Input.GetKeyDown(KeyCode.Z) || willJump) && (isGrounded || coyoteTime))
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

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag != "Enemy")
            return;

        // TO DO: normalize vector (ale zostawione tymczasowo)

        Vector2 pushDir = transform.position - collision.transform.position;
        rb.AddForce(pushDir * 20, ForceMode2D.Impulse);
        numbState = true;
        Invoke(nameof(noNumb), 0.2f);

        blinksImage();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag != "Enemy")
            return;

        // TO DO: normalize vector (ale zostawione tymczasowo)

        Vector2 pushDir = transform.position - collision.transform.position;
        rb.AddForce(pushDir * 20, ForceMode2D.Impulse);
        numbState = true;
        Invoke(nameof(noNumb), 0.2f);

        blinksImage();
    }

    void noNumb()
    {
        numbState = false;
    }

    void blinksImage()
    {
        imageRed.SetActive(true);
        Invoke(nameof(HideImage), 0.1f);
    }

    void HideImage()
    {
        imageRed.SetActive(false);
    }

}
