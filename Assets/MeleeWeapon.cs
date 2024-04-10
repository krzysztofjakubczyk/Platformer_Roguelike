using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Enemy")
        {
            return;
        }

        // przykladowe wartosci
        AttackDetails attackDetails = new AttackDetails();
        attackDetails.position = transform.position;
        attackDetails.damageAmount = 5;
        attackDetails.stunDamageAmount = 5;

        collision.transform.parent.GetComponent<Entity>().DamageGet(attackDetails);
    }

}
