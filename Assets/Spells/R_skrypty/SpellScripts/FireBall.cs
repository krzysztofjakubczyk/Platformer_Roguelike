using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;


public class FireBall : Spell
{
    [SerializeField] float damageOnExplode;
    [SerializeField] float pushbackForce;
    [SerializeField] float dmgRadius;
    [SerializeField] float speed;
    [SerializeField] LayerMask enemyLayer;

    private void Start()
    {
        base.Start();
        Invoke(nameof(DestroyObject), 5);
    }

    void Update()
    {
        rb.velocity = castDirection.normalized * speed * Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.tag != "Enemy" && collision.tag != "Player")
        //{
        //    Attack();

        //    return;
        //}


        //if (collision.tag != "Enemy")
        //{
        //    print(collision.name);
        //    return;
        //}

        if (collision.tag == "Enemy")
            Attack();
    }

    public override void Attack()
    {
        Collider2D[] closeEnemies = Physics2D.OverlapCircleAll(transform.position, dmgRadius, enemyLayer);

        // przykladowe wartosci
        AttackDetails attackDetails = new AttackDetails();
        attackDetails.position = transform.position;
        attackDetails.damageAmount = damage;
        attackDetails.stunDamageAmount = stunDamage;

        foreach (Collider2D c in closeEnemies)
        {
            if (c.transform.parent != null)
            {
                if (c.transform.parent.GetComponent<Entity>() != null)
                {
                    c.transform.parent.GetComponent<Entity>().DamageGet(attackDetails);
                }
                else if (c.GetComponent<HealthController>() != null)
                {
                    c.GetComponent<HealthController>().SubAmount(damage);
                }
            }
        }
        
        
        Destroy(gameObject);
    }

    public void moreDMG( float m)
    {
        damage += m;
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
