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
            weapon.SetActive(true);
            animator.SetBool("isAttacking", true);
            Invoke(nameof(OffWeapon), 0.1f);
        }
    }

    void OffWeapon()
    {
        weapon.SetActive(false);
        animator.SetBool("isAttacking", false);
    }
}
