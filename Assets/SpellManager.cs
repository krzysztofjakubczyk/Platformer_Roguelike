using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public Spell spell;
    public GameObject activeSpell;

    StaminaControl StaminaControl;

    float cost;
    float damage;

    // Start is called before the first frame update
    void Start()
    {
        StaminaControl = GetComponent<StaminaControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if(StaminaControl.currentStamina > cost)
                Instantiate(activeSpell);
        }
    }

    public void setSpell(int spellNumber)
    {
          
    }
}
