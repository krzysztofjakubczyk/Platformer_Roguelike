using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class moveSnake : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    [SerializeField] GameObject player;
    [SerializeField] LayerMask ground;

    [SerializeField] float speed;
    [SerializeField] float walkTime;
    [SerializeField] float idleTime = 5;
    [SerializeField] float throwPower;
    float IdleTimeLeft;
    bool running, runningRight;
    bool jumped;
    Color basicColor;

    PolygonCollider2D polcoll;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        polcoll = GetComponent<PolygonCollider2D>();

        IdleTimeLeft = idleTime;

        basicColor = GetComponent<SpriteRenderer>().color;
    }


    void Update()
    {
        if (IsGrounded())
            animator.SetBool("grounded", true);
        else
            animator.SetBool("grounded", false);

        if (jumped)
        {
            if (IsGrounded())
            {
                if (player.transform.position.x - transform.position.x > 0)
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                else
                    transform.rotation = Quaternion.Euler(0, 180, 0);

                jumped = false;
            }
        }


        if (IdleTimeLeft <= 0)
        {
            int attackNow = Random.Range(1, 5);
            switch (attackNow)
            {
                case 1:
                    animator.SetTrigger("armAttack");
                    break;
                case 2:
                    animator.SetTrigger("bladesAttack");
                    ThrowBlades();
                    break;
                case 3:
                    animator.SetBool("chargeAttack", true);
                    Invoke(nameof(StartSpeed), 1f);
                    break;
                case 4:
                    JumpAttack();
                    break;
            }

            IdleTimeLeft = idleTime;
        }

        IdleTimeLeft -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (running)
        {
            if (runningRight)
                rb.velocity = Vector2.right * speed;
            else
                rb.velocity = Vector2.left * speed;
        }
            
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 2.5f, ground);
    }

    void ThrowBlades()
    {
        GameObject throwdart = Instantiate(transform.GetChild(0).gameObject);
        throwdart.GetComponent<throwMove>().dir = new Vector2(-1, 0);
        throwdart.transform.parent = gameObject.transform;
        throwdart.transform.localScale = Vector3.one;
        throwdart.transform.position = transform.GetChild(0).position;
        throwdart.SetActive(true);

        GameObject throwdart2 = Instantiate(transform.GetChild(0).gameObject);
        throwdart2.GetComponent<throwMove>().dir = new Vector2(-1, 1);
        throwdart2.transform.parent = gameObject.transform;
        throwdart2.transform.localScale = Vector3.one;
        throwdart2.transform.position = transform.GetChild(0).position;
        throwdart2.SetActive(true);

        GameObject throwdart3 = Instantiate(transform.GetChild(0).gameObject);
        throwdart3.GetComponent<throwMove>().dir = new Vector2(-1, 3);
        throwdart3.transform.parent = gameObject.transform;
        throwdart3.transform.localScale = Vector3.one;
        throwdart3.transform.position = transform.GetChild(0).position;
        throwdart3.SetActive(true);
    }

    void JumpAttack()
    {
        Vector2 throwDir;
        if (player.transform.position.x - transform.position.x > 0)
            throwDir = new Vector2(1, 1.5f);
        else
            throwDir = new Vector2(-1, 1.5f);

        GetComponent<Rigidbody2D>().AddForce(throwDir * throwPower, ForceMode2D.Impulse);
        Invoke(nameof(SetJumped), 0.2f);
    }

    void StartSpeed()
    {
        animator.SetBool("chargeAttack", false);
        animator.SetBool("running", true);
        speed = 10;
        running = true;
        if (player.transform.position.x - transform.position.x > 0)
            runningRight = true;
        else
            runningRight = false;

        Invoke(nameof(StopSpeed), 2);
    }

    void StopSpeed()
    {
        running = false;
        speed = 0;
        animator.SetBool("running", false);
        if (player.transform.position.x - transform.position.x > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    void SetJumped()
    {
        jumped = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, Vector2.down * 2.5f);
    }


}
/*
if (Input.GetKeyDown(KeyCode.K))
{
    animator.SetTrigger("armHit");
}

if (Input.GetKeyDown(KeyCode.J))
{
    GameObject throwdart = Instantiate(transform.GetChild(0).gameObject);
    throwdart.transform.parent = gameObject.transform;
    throwdart.transform.localScale = Vector3.one;
    throwdart.transform.position = transform.GetChild(0).position;
    throwdart.SetActive(true);
}
*/