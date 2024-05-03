using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float stunDamage;
    [SerializeField] float attackRate;
    [SerializeField] float attackDelay;

    bool canAttack = true;
    AttackDetails attackDetails = new AttackDetails();


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
        print(0);
        StartCoroutine(Attack(collision));
    }

    void allowAttack()
    {
        canAttack = true;
    }

    IEnumerator Attack(Collider2D col)
    {
        print(1);
        yield return new WaitForSeconds(attackDelay);
        print(2);
        col.transform.parent.GetComponent<Entity>().DamageGet(attackDetails);
    }

}
