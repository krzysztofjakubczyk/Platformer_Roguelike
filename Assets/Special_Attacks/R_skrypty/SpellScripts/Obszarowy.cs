using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obszarowy : Spell
{
    [SerializeField] float delay;
    [SerializeField] float radius;
    [SerializeField] float maxThrowPower;
    [SerializeField] float loadPowerSpeed;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] protected ParticleSystem effectBoom;
    Vector2 throwDir;
    
    float throwPower = 1;

    bool doneOnce;
//start timer in start
// check if x up - wtedy koniec czasu - przerobic to na odleglosc rzutu - add max cap

    void Start()
    {
        base.Start();
        //Invoke(nameof(Attack), delay);
        GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(SelectPower());

    }


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.X) && !doneOnce)
        {
            doneOnce = true;
            GetComponent<SpriteRenderer>().enabled = true;
            transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 1);

            Vector2 throwDir;
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                throwDir = new Vector2(0, 2);
            else if(player.GetComponent<SpriteRenderer>().flipX)
                throwDir = new Vector2(1, 1);
            else
                throwDir = new Vector2(-1, 1);


            GetComponent<Rigidbody2D>().AddForce(throwDir * throwPower, ForceMode2D.Impulse);

            Invoke(nameof(Explode), 3);
        }
    }



    public override void Attack()
    {
        //Collider2D[] targets = Physics2D.OverlapCircleAll(player.transform.position, radius, enemieLayer);

        //foreach (Collider2D target in targets)
        //{
        //    Vector2 pushdir = target.transform.position - player.transform.position;

        //    target.GetComponent<Rigidbody2D>().AddForce(pushdir.normalized * 30, ForceMode2D.Impulse);
        //}

        //Destroy(gameObject);
    }

    IEnumerator  SelectPower()
    {
        while (throwPower < maxThrowPower && !doneOnce)
        {
            throwPower += 0.1f;
            yield return new WaitForSeconds(loadPowerSpeed);
        }
    }

    void Explode()
    {
        Instantiate(effectBoom, transform.position, Quaternion.identity);
        Collider2D[] closeEnemies = Physics2D.OverlapCircleAll(transform.position, radius, enemyLayer);
        foreach(Collider2D enemy in closeEnemies)
        {
            AttackDetails attackDetails = new AttackDetails();
            attackDetails.position = transform.position;
            attackDetails.damageAmount = damage;
            attackDetails.stunDamageAmount = stunDamage;
            enemy.transform.parent.GetComponent<Entity>().DamageGet(attackDetails);
        }
        Destroy(gameObject);
    }
}
