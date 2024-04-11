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

    private void Start()
    {
        stopPos = player.transform.position;
    }

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

        StartCoroutine(GoBackToPlayer(collision));

    }

    public override void Attack()
    {

    }
    IEnumerator GoBackToPlayer(Collider2D target)
    {
        Vector2 dir2 = new Vector2(transform.position.x - target.transform.position.x, 0).normalized;
       
        float gravityof = target.GetComponent<Rigidbody2D>().gravityScale;
        target.GetComponent<Rigidbody2D>().gravityScale = 0;

        shotAlready = true;

        while (Mathf.Abs(target.transform.position.x - stopPos.x) > stopXDys)
        {
            rb.velocity = (dir2 * speed) * Time.deltaTime;
            target.GetComponent<Rigidbody2D>().velocity = (dir2 * speed) * Time.deltaTime;

            yield return null;
        }

        target.GetComponent<Rigidbody2D>().gravityScale = gravityof;
        target.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        Destroy(gameObject);
    }
    
}
