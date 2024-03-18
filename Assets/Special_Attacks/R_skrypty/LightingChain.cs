using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LightingChain : MonoBehaviour
{
    [SerializeField]GameObject player;
    [SerializeField] float staminaCost;
    [SerializeField] float damage;
    [SerializeField] float searchRadius;
    [SerializeField] float jumpsNumberMax;
    [SerializeField] float speed;
    [SerializeField] LayerMask enemyLayer;

    [SerializeField]float jumpsNumberCurrent;

    Vector2 dir;

    List<string> attacked = new List<string>();

    Rigidbody2D rb;

    Collider2D closestEnemy = null;

    void Start()
    {
        dir = Vector2.right;

        rb = GetComponent<Rigidbody2D>();
    }



    void Update()
    {
        if(closestEnemy != null)
            dir = closestEnemy.transform.position - transform.position;

        rb.velocity = dir.normalized * speed * Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.tag != "Enemy" && collision.tag != "Player")
            //Destroy(gameObject);

        if (collision.tag != "Enemy")            
            return;

        foreach (string nm in attacked)
        {
            if (collision.name == nm)
            {
                print(nm);
                return;

                
            }
        }

        jumpsNumberCurrent++;

        if (jumpsNumberCurrent >= jumpsNumberMax)
        {
            Destroy(gameObject);
            return;
        }


        // funkcja zadajaca dmg przeciwnikowi here

        attacked.Add(collision.name);

        Collider2D[] closeEnemies = Physics2D.OverlapCircleAll(transform.position, searchRadius, enemyLayer);

        float minDistance = 100;
        bool goOn = false;
        

        foreach (Collider2D col in closeEnemies)
        {
            foreach (string nm in attacked)
            {
                if (col.name == nm)
                {
                    goOn = true;
                    break;
                }      
            }

            if (goOn)
            {
                goOn = false;
                continue;
            }

            if ((transform.position - col.transform.position).magnitude < minDistance)
            {  
                minDistance = (transform.position - col.transform.position).magnitude;
                closestEnemy = col;
                
            }
        }

    }

    void ChainAttacks()
    {
        // tutaj funkcja z OnTrigger
    }
}
