using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu]
public class FireBall : Spell
{
    //[SerializeField] GameObject player;
    //[SerializeField] float staminaCost;
    //[SerializeField] float damage;
    //[SerializeField] float damageOnExplode;
    //[SerializeField] float pushbackForce;
    //[SerializeField] float dmgRadius;
    //[SerializeField] float speed;
    //[SerializeField] LayerMask enemyLayer;

    //Vector2 dir;

    //Rigidbody2D rb;

    //void Start()
    //{
    //    // TO DO ustawic kierunek rzucania

    //    dir = player.transform.right;

    //    //rb = GetComponent<Rigidbody2D>();
    //}



    //void Update()
    //{
    //    rb.velocity = dir.normalized * speed * Time.deltaTime;
    //}

    public override void Activate(GameObject parent)
    {
        //base.Activate(parent);
        Debug.Log("wtf");

    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag != "Enemy" && collision.tag != "Player")
    //        //BlowUpDamage();
    //        // wybuch 


    //    if (collision.tag != "Enemy")
    //        return;


    //    // funkcja zadajaca dmg przeciwnikowi here
    //    //BlowUpDamage();

    //}

    //void BlowUpDamage()
    //{
    //    Collider2D[] closeEnemies = Physics2D.OverlapCircleAll(transform.position, dmgRadius, enemyLayer);

    //    foreach (Collider2D c in closeEnemies)
    //    {
    //        // zadawanie obrazen oraz
    //        //odpychanie:
    //        Vector2 dir2 = c.transform.position - transform.position;
    //        c.GetComponent<Rigidbody2D>().AddForce(dir2 * pushbackForce, ForceMode2D.Impulse);
    //    }

    //   // Destroy(gameObject);
    //}
}
