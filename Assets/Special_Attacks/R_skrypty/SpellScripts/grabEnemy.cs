using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabEnemy : Spell
{
    [SerializeField] float pullForce;
    [SerializeField] float speed;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] bool maxVersion;
    bool shotAlready;


    void Update()
    {
        if(!shotAlready)
            rb.velocity = castDirection.normalized * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Enemy" && collision.tag != "Player")
            Destroy(gameObject);

        if (collision.tag != "Enemy")
            return;

        //Vector2 dir2 = transform.position - collision.transform.position;
        //collision.GetComponent<Rigidbody2D>().AddForce(dir2 * pullForce, ForceMode2D.Impulse);

        //Destroy(gameObject);

        // druga wersja - przyciagniecie do gracza


        StartCoroutine(GoBackToPlayer(collision));

    }

    public override void Attack()
    {

    }
    IEnumerator GoBackToPlayer(Collider2D target)
    {
        Vector2 dir2 = (transform.position - target.transform.position).normalized;

        target.GetComponent<Rigidbody2D>().gravityScale = 0;

        shotAlready = true;

        while (transform.position.x > player.transform.position.x + 1)
        {
            rb.velocity = (dir2 * speed) * Time.deltaTime;
            target.GetComponent<Rigidbody2D>().velocity = (dir2 * speed) * Time.deltaTime;

            yield return null;
        }

        target.GetComponent<Rigidbody2D>().gravityScale = 1;
        Destroy(gameObject);
    }
    
}
