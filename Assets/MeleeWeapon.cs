using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float stunDamage;
    [SerializeField] float attackRate;
    [SerializeField] float attackDelay;

    bool canAttack = true;

    GameObject player;
    Animator animator;

    AttackDetails attackDetails = new AttackDetails();

    void Start()
    {
        player = transform.parent.gameObject;
        animator = player.GetComponent<Animator>();

        GetComponent<BoxCollider2D>().enabled = false;
    }


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            if(canAttack)
                animator.SetTrigger("Attacking");

            Invoke(nameof(OnWeapon), attackDelay);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Enemy" || !canAttack)
        {
            return;
        }

        canAttack = false;
        Invoke(nameof(allowAttack), attackRate);
        
        attackDetails.position = transform.position;
        attackDetails.damageAmount = damage;
        attackDetails.stunDamageAmount = stunDamage;

        collision.transform.parent.GetComponent<Entity>().DamageGet(attackDetails);

        //StartCoroutine(Attack(collision));
    }

    void allowAttack()
    {
        canAttack = true;
    }

    void OnWeapon()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        Invoke(nameof(OffWeapon), 0.1f);
    }

    void OffWeapon()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

}
/*
IEnumerator Attack(Collider2D col)
{
    yield return new WaitForSeconds(attackDelay);
    col.transform.parent.GetComponent<Entity>().DamageGet(attackDetails);


}
*/