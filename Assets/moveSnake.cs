using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class moveSnake : MonoBehaviour
{
    public GameObject player;
    [SerializeField] LayerMask ground;

    [SerializeField] float speed = 10;
    [SerializeField] float walkTime;
    [SerializeField] float idleTime = 5;
    [SerializeField] float throwPower;

    Rigidbody2D rb;
    Animator animator;

    float IdleTimeLeft;
    bool running, runningRight;
    bool jumped;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        IdleTimeLeft = idleTime;
    }


    void Update()
    {
        if (IsGrounded())
            LookAtPlayer();   

        if (IdleTimeLeft <= 0)
        {
            int attackNow = Random.Range(2, 3);

            switch (attackNow)
            {
                case 1:
                    animator.SetTrigger("armAttack");
                    Invoke(nameof(ArmAttack), 0.7f);
                    break;
                case 2:
                    animator.SetTrigger("bladesAttack");
                    Invoke(nameof(ThrowBlades), 1f);
                    break;
                case 3:
                    animator.SetBool("chargeAttack", true);
                    Invoke(nameof(StartSpeed), 1.5f);
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
        if (Physics2D.Raycast(transform.position, Vector2.down, 2.5f, ground))
        {
            animator.SetBool("grounded", true);
            return true;
        }
        else
        {
            animator.SetBool("grounded", false);
            return false;
        }
    }

    void ArmAttack()
    {
        GetComponent<PolygonCollider2D>().offset = new Vector2(0.3f, 0);
        Invoke(nameof(EndArmAttack), 0.1f);
    }

    void EndArmAttack()
    {
        GetComponent<PolygonCollider2D>().offset = Vector2.zero;
    }

    void ThrowBlades()
    {
        List<GameObject> blades = new List<GameObject>();
        GameObject blade1 = Instantiate(transform.GetChild(0).gameObject);
        GameObject blade2 = Instantiate(transform.GetChild(0).gameObject);
        GameObject blade3 = Instantiate(transform.GetChild(0).gameObject);

        blades.Add(blade1);
        blades.Add(blade2);
        blades.Add(blade3);

        foreach(GameObject blade in blades)
        {
            blade.transform.localScale = gameObject.transform.localScale;
            blade.transform.rotation = transform.GetChild(0).rotation;
            blade.transform.position = transform.GetChild(0).position;
            blade.GetComponent<throwMove>().player = player;
            blade.SetActive(true);
        }

        blade1.GetComponent<throwMove>().dir = transform.right;
        blade2.GetComponent<throwMove>().dir = new Vector2(transform.right.x, 1);
        blade3.GetComponent<throwMove>().dir = transform.up;

    }

    void JumpAttack()
    {
        Vector2 throwDir;
        if (player.transform.position.x - transform.position.x > 0)
            throwDir = new Vector2(1, 1.5f);
        else
            throwDir = new Vector2(-1, 1.5f);

        GetComponent<Rigidbody2D>().AddForce(throwDir * throwPower, ForceMode2D.Impulse);
    }

    void StartSpeed()
    {
        animator.SetBool("chargeAttack", false);
        animator.SetBool("running", true);
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
        rb.velocity = Vector2.zero;
        animator.SetBool("running", false);

        LookAtPlayer();
    }

    void LookAtPlayer()
    {
        if (player.transform.position.x - transform.position.x > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    void CheckIfWalls()
    {
        if (Physics2D.Raycast(transform.position, Vector2.right, 2f, ground) || Physics2D.Raycast(transform.position, Vector2.left, 2f, ground))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            LookAtPlayer();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, Vector2.down * 2.5f);
        Gizmos.DrawRay(transform.position, Vector2.right * 2f);
        Gizmos.DrawRay(transform.position, Vector2.left * 2f);
    }


}