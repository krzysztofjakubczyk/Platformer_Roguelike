using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LightingChain : Spell
{
    [SerializeField] float searchRadius;
    [SerializeField] float jumpsNumberMax;
    [SerializeField] float speed;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] LayerMask wallLayer;

    [SerializeField]float jumpsNumberCurrent;

    List<int> alreadyAttacked = new List<int>();

    Collider2D closestEnemy = null;


    void Update()
    {
        if (closestEnemy != null)
        {
            castDirection = closestEnemy.transform.position - transform.position;
        }

        rb.velocity = castDirection.normalized * speed * Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground")){
            Destroy(gameObject);
            return;
        }

        if (collision.tag != "Enemy")
            return;

        //edge case ktory nie powinien wystapic (wiec nie obslugiwany):
        //miedzy przeciwnikami moze pojawic sie przeciwnik, ktory wczesniej byl juz trafiony - w takim wymadku ignorowac kolizje z nim (niezaimplementowane)

        AttackDetails attackDetails = new AttackDetails();
        attackDetails.position = transform.position;
        attackDetails.damageAmount = damage;
        attackDetails.stunDamageAmount = stunDamage;
        collision.transform.parent.GetComponent<Entity>().DamageGet(attackDetails);
        
        alreadyAttacked.Add(collision.GetInstanceID());

        jumpsNumberCurrent++;
        if (jumpsNumberCurrent >= jumpsNumberMax)
        {
            Destroy(gameObject);
            return;
        }
 
        // wyszukiwanie kolejnych przeciwnikow
        Collider2D[] closeEnemies = Physics2D.OverlapCircleAll(transform.position, searchRadius, enemyLayer);

        float minDistance = 100;
        bool skip = false;
        
        foreach (Collider2D col in closeEnemies)
        {
            foreach (int name in alreadyAttacked)
            {
                if (col.GetInstanceID() == name)
                {
                    skip = true;
                    break;
                }      
            }
            // uzyc linecast zamiast raycast
            if (Physics2D.Raycast(transform.position, col.transform.position - transform.position, wallLayer))
            {
                print(col.transform.position - transform.position);
                skip = true;
                print("wall");
            }

            if (skip)
            {
                skip = false;
                continue;
            }

            if ((transform.position - col.transform.position).magnitude < minDistance)
            {
                minDistance = (transform.position - col.transform.position).magnitude;
                closestEnemy = col;
                
            }
        }

    }

    public override void Attack()
    {
        // tutaj funkcja z OnTrigger
    }
}
