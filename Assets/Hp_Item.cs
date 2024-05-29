using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Hp_Item : ItemOnShop
{
    public List<StatInfo> statInfo = new();
    [SerializeField] int testowy = 5;

    public override void UseFunction(float x)
    {
        player.GetComponent<PlayerStats>().ChangeMaxHp(x);
        
    }
}

