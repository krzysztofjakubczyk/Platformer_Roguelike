using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [SerializeField]GameObject activeSpell;
    [SerializeField]float yOffset;
    StaminaControl StaminaControl;

    float cost;
    Vector2 throwDir;
    bool canCast;


    void Start()
    {
        StaminaControl = GetComponent<StaminaControl>();

        // TO DO:       ustawic dir podczas rzucania a nie w start
        throwDir = transform.right;

        canCast = true;
    }


    void Update()
    {
        // TO DO:       zmienna zamiast keycode

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            throwDir = transform.up;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            throwDir = transform.right;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            throwDir = -transform.right;
        }


        if (Input.GetKeyDown(KeyCode.X) && activeSpell != null && canCast)
        {
            canCast = false;
            float chargeTime = activeSpell.GetComponent<Spell>().reachargeTime;
            Invoke(nameof(CanCastAgain), chargeTime);

            cost = activeSpell.GetComponent<Spell>().cost;

            if (StaminaControl.GetCurrentAmount() > cost)
            {
                StaminaControl.SubAmount(cost);
                    
                activeSpell.GetComponent<Spell>().castDirection = throwDir;
                activeSpell.GetComponent<Spell>().player = gameObject;
                activeSpell.GetComponent<Spell>().rb = activeSpell.GetComponent<Rigidbody2D>();

                Vector2 startPos = new Vector2(transform.position.x, transform.position.y + yOffset);
                GameObject newSpell = Instantiate(activeSpell, startPos,Quaternion.identity);

                if (throwDir.y == transform.up.y)
                    newSpell.transform.localRotation = Quaternion.Euler(0, 0, 90);
                else if (throwDir.x == -transform.right.x)
                    newSpell.GetComponent<SpriteRenderer>().flipX = true;


            }
        }
    }

    public void setSpell(GameObject spell)
    {
          activeSpell = spell;
    }

    void CanCastAgain()
    {
        canCast = true;
    }
}
