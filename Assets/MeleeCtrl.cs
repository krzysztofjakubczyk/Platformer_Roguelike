using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCtrl : MonoBehaviour
{
    GameObject weapon;
    Animator animator;

    void Start()
    {
        weapon = transform.GetChild(0).gameObject;
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            weapon.GetComponent<BoxCollider2D>().enabled = true;
            animator.SetTrigger("Attacking");
            Invoke(nameof(OffWeapon), 0.1f);
        }
    }

    void OffWeapon()
    {
        weapon.GetComponent<BoxCollider2D>().enabled = false;

    }

}
