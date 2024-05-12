using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeSpellTest : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject spell1;
    [SerializeField] GameObject spell2;
    [SerializeField] GameObject spell3;
    [SerializeField] GameObject spell4;
    [SerializeField] GameObject spell5;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1) && spell1 != null)
        {
            player.GetComponent<SpellManager>().setSpell(spell1);
            print(spell1.name + "  SET");
        }
        if (Input.GetKeyUp(KeyCode.Alpha2) && spell2 != null)
        {
            player.GetComponent<SpellManager>().setSpell(spell2);
            print(spell2.name + "  SET");
        }
        if (Input.GetKeyUp(KeyCode.Alpha3) && spell1 != null)
        {
            player.GetComponent<SpellManager>().setSpell(spell3);
            print(spell3.name + "  SET");
        }
        if (Input.GetKeyUp(KeyCode.Alpha4) && spell1 != null)
        {
            player.GetComponent<SpellManager>().setSpell(spell4);
            print(spell4.name + "  SET");
        }
        if (Input.GetKeyUp(KeyCode.Alpha5) && spell1 != null)
        {
            player.GetComponent<SpellManager>().setSpell(spell5);
            print(spell5.name + "  SET");
        }
    }

}
