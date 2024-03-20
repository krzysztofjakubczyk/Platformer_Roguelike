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


    void Update()
    {
        rb.velocity = castDirection.normalized * speed * Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Enemy" && collision.tag != "Player")
            Attack();


        if (collision.tag != "Enemy")
            return;


        // zadanie dmg dla pierwszy target here

        Attack();

    }

    public override void Attack()
    {
        Collider2D[] closeEnemies = Physics2D.OverlapCircleAll(transform.position, dmgRadius, enemyLayer);

        foreach (Collider2D c in closeEnemies)
        {
            // zadawanie obrazen oraz
            //odpychanie:
            Vector2 dir2 = c.transform.position - transform.position;
            c.GetComponent<Rigidbody2D>().AddForce(dir2 * pushbackForce, ForceMode2D.Impulse);
        }

        Destroy(gameObject);
    }
}
