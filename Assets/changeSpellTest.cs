using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeSpellTest : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject spell;

    bool canTakeSpell;


    // Update is called once per frame
    void Update()
    {
        if (canTakeSpell)
        {
            if (Input.GetKeyUp(KeyCode.Q))
            {
                player.GetComponent<SpellManager>().setSpell(spell);
                print(spell.name + "  SET");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
            canTakeSpell = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
            canTakeSpell = false;
    }
}
