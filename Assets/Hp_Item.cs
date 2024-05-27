using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hp_Item : ItemOnShop
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;


        playerIsClose = true;

        name.text = m_ScriptableObject.Name;
        name.text += "  Cost: " + m_ScriptableObject.Cost.ToString();
        description.text = m_ScriptableObject.Description;
        spriteRenderer.sprite = m_ScriptableObject.ImageItem;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerIsClose = false;
    }

    public override void UseFunction(float x)
    {
        print(player.name);
        player.GetComponent<PlayerStats>().ChangeMaxHp(x);
    }
}
