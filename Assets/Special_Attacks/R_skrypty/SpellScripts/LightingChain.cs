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

        //edge case, ktory nie powinien wystapic (wiec nie obslugiwany):
        //miedzy przeciwnikami moze pojawic sie przeciwnik, ktory wczesniej byl juz trafiony - w takim wypadku ignorowac kolizje z nim (niezaimplementowane)

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
        bool foundOne = false;
        foreach (Collider2D col in closeEnemies)
        {
            foreach (int name in alreadyAttacked)       // spr czy enemy byl juz atakowany
            {
                if (col.GetInstanceID() == name)
                {
                    skip = true;
                    break;
                }      
            }

            if (Physics2D.Linecast(transform.position, col.transform.position, wallLayer))      // spr czy miedzy pociskiem a enemy jest sciana
            {
                skip = true;
            }

            if (skip)
            {
                skip = false;
                continue;
            }
            else
                foundOne = true;

            if ((transform.position - col.transform.position).magnitude < minDistance)
            {
                minDistance = (transform.position - col.transform.position).magnitude;
                closestEnemy = col;
                
            }
        }
        // jesli nie wykryto nowego przeciwnika to usun pocisk
        if (!foundOne) Destroy(gameObject);

    }

    public override void Attack()
    {
        // tutaj funkcja z OnTrigger
    }
}
