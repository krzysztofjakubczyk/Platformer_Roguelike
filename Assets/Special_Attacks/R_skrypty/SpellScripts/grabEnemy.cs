using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabEnemy : Spell
{
    [SerializeField] float pullForce;
    [SerializeField] float speed;
    [SerializeField] float stopXDys;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] bool maxVersion;
    bool shotAlready;

    Vector2 stopPos;

    bool hasTimeLeft;

    private void Start()
    {
        base.Start();
        stopPos = player.transform.position;
        Debug.Log("rzucam spella przyci¹gania");
    }

    void Update()
    {
        if(!shotAlready)
            rb.velocity = castDirection.normalized * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy") StartCoroutine(GoBackToPlayer(collision));
    }

    public override void Attack()
    {

    }
    IEnumerator GoBackToPlayer(Collider2D target)
    {
        AttackDetails attackDetails = new AttackDetails();
        attackDetails.position = transform.position;
        attackDetails.damageAmount = 5;
        attackDetails.stunDamageAmount = 105;
        //to powy¿ej fajnie jakby by³o w funkcji ataku

        target.transform.parent.GetComponent<Entity>().DamageGet(attackDetails);
        Vector2 dir2 = new Vector2(transform.position.x - target.transform.position.x, 0).normalized;
       
        float gravityof = target.GetComponent<Rigidbody2D>().gravityScale;
        target.GetComponent<Rigidbody2D>().gravityScale = 0;

        shotAlready = true;

        hasTimeLeft = true;
        Invoke(nameof(TimeEnd), 3);

        while (Mathf.Abs(target.transform.position.x - stopPos.x) > stopXDys && hasTimeLeft)
        {
            rb.velocity = (dir2 * speed) * Time.deltaTime;
            target.GetComponent<Rigidbody2D>().velocity = (dir2 * speed) * Time.deltaTime;
            //strasznie szybko leci mo¿na coœ pobowiæ siê z wartoœciami
            yield return null;
        }

        target.GetComponent<Rigidbody2D>().gravityScale = gravityof;
        target.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        Destroy(gameObject);
       
    }

    void TimeEnd()
    {
        hasTimeLeft = false;
    }
    
}
