using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obszarowy : Spell
{
    [SerializeField] float delay;
    [SerializeField] float radius;
    [SerializeField] LayerMask enemieLayer;
    [SerializeField] float maxThrowPower;
    [SerializeField] float loadPowerSpeed;
    Vector2 throwDir;
    
    float throwPower = 1;

    bool doneOnce;
//start timer in start
// check if x up - wtedy koniec czasu - przerobic to na odleglosc rzutu - add max cap

    void Start()
    {
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
            GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * throwPower, ForceMode2D.Impulse);
            print(throwPower);
            Invoke(nameof(kill), 3);
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

    void kill()
    {
        Destroy(gameObject);
    }
}
