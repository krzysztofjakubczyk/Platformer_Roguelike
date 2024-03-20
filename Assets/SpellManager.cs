using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [SerializeField]GameObject activeSpell;
    StaminaControl StaminaControl;

    float cost;
    Vector2 throwDir;


    void Start()
    {
        StaminaControl = GetComponent<StaminaControl>();

        // TO DO:       ustawic dir podczas rzucania a nie w start
        throwDir = transform.right;
    }


    void Update()
    {
        // TO DO:       zmienna zamiast keycode

        if (Input.GetKeyDown(KeyCode.C) && activeSpell != null)
        {
            cost = activeSpell.GetComponent<Spell>().cost;

            if (StaminaControl.currentStamina > cost)
            {
                StaminaControl.SubStamina(cost)
                    ;
                activeSpell.GetComponent<Spell>().castDirection = throwDir;
                activeSpell.GetComponent<Spell>().player = gameObject;
                activeSpell.GetComponent<Spell>().rb = activeSpell.GetComponent<Rigidbody2D>();

                Instantiate(activeSpell, transform.position,Quaternion.identity);

            }
        }
    }

    public void setSpell(GameObject spell)
    {
          activeSpell = spell;
    }
}
