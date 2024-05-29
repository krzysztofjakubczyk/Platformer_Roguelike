using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "NewStats", menuName = "Stats")]

public class PlayerStatsv2 : ScriptableObject
{
    public float hpCurrent = 100;
    public float hpMax = 100;
    public float hpRecoverTime = 0.1f;
    public float manaCurrent = 100;
    public float manaMax = 100;
    public float manaRecoverTime = 0.5f;
    public float damage = 5;
    public float attackSpeed = 0.5f;
    public float fireballDmg = 5;
    public float fireballSpeed = 500;
    public float lightningDmg = 5;
    public float lightningSpeed = 500;
    public float grabDmg = 5;
    public float grabSpeed = 500;
    public float laserDmg = 50;
    public float laserSpeed = 500;
    public float grenadeDmg = 5;
    public float grenadeSpeed = 50;
    public float jumpHeight = 16;
    public float moveSpeed = 5;
    public float dashPower = 10;

    //Dictionary<string, float> playerStats = new Dictionary<string, float>();
    
    void Awake()
    {

    }

}
