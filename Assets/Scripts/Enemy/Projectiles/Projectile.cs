using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private AttackDetails attackDetails;
    private Entity entity;
    private float speed;
    private float travelDistance;
    private float xStartPos;
    private Vector3 direction;

    [SerializeField]
    private float gravity;
    [SerializeField]
    private float damageRadius;

    private bool isGravityOn;
    private bool hasHitGround;

    private Rigidbody2D rb;

    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private LayerMask whatIsPlayer;
    [SerializeField]
    private Transform damagePosition;

    float angle;
    float angleBeforeHit;
    [SerializeField]
    private bool isBoomed = false;
    [SerializeField]
    private GameObject projectileShot;
    [SerializeField]
    private float boomRadius;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0.0f;
        rb.velocity = direction * speed;

        isGravityOn = false;
        xStartPos = transform.position.x;

        //Vector3 directionToShoot = entity.aliveGameObject.transform.position - transform.position;
    }
    private void Update()
    {
        if (!hasHitGround) {
            attackDetails.position = transform.position;

            angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;

            if (isGravityOn)
            {
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
           
    }
    private void FixedUpdate()
    {
        if (!hasHitGround)
        {
            Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsPlayer);
            Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);

            if (damageHit)
            {
                //damageHit.transform.SendMessage("Damage", attackDetails);
                if (isBoomed) Boom();
                else entity.playerHp.DamagePlayer(attackDetails.damageAmount);//ale jest to strasznie nieresponsywne ale nie wiem czy eventy w tym przypadku to dobry pomys³
                Destroy(gameObject);
            }
            if (groundHit)
            {
                hasHitGround = true;
                rb.gravityScale = 0f;
                angleBeforeHit = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                rb.velocity = Vector2.zero;
                rb.isKinematic = true;

                if(isBoomed)Invoke(nameof(Boom), 3);
                Destroy(gameObject,3);
               
            }

            if (Mathf.Abs(xStartPos - transform.position.x) >= travelDistance && !isGravityOn)
            {
                isGravityOn = true;
                rb.gravityScale = gravity;
            }
        }
        //Rotate(angleBeforeHit);
    }
    public void FireProjectile(float speed,float travelDistance, float damage, Vector3 direction, Entity entity)
    {
        this.speed = speed;
        this.travelDistance = travelDistance;
        this.direction = direction;
        this.entity = entity;
        attackDetails.damageAmount = damage;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
        Gizmos.DrawWireSphere(damagePosition.position, boomRadius);
    }
    private void Rotate(float angle)
    {
        Debug.Log(angle + " angle");

        if (angle != 0) {
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        

        Debug.Log(angle + " angle after");
    }
    private void Boom()
    {
        GameObject Projectile = Instantiate(projectileShot,damagePosition.position,Quaternion.identity);
        Collider2D playerHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsPlayer);
        if (playerHit)
        {
            print("deal dmg");
            entity.playerHp.DamagePlayer(attackDetails.damageAmount);
        }
        Destroy(Projectile, 5);
    }
}
