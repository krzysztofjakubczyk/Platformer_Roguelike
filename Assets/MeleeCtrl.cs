using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCtrl : MonoBehaviour
{
    GameObject weapon;

    void Start()
    {
        weapon = transform.GetChild(0).gameObject;
    }


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            weapon.SetActive(true);
            Invoke(nameof(OffWeapon), 0.1f);
        }
    }

    void OffWeapon()
    {
        weapon.SetActive(false);
    }
}
