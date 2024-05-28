using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hp_Item : ItemOnShop
{
    public override void UseFunction(float x)
    {
        player.GetComponent<PlayerStats>().ChangeMaxHp(x);
    }
}
