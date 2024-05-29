using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    GameObject player;
    public event Action updateGUI;

    [SerializeField]PlayerStatsv2 stats;
    public Dictionary<PlayerStat, float> playerStats = new Dictionary<PlayerStat, float>();


    void Awake()
    {
        playerStats.Add(PlayerStat.hpCurrent, stats.hpCurrent);
        playerStats.Add(PlayerStat.hpMax, stats.hpMax);
        playerStats.Add(PlayerStat.hpRecoverTime, stats.hpRecoverTime);
        playerStats.Add(PlayerStat.manaCurrent, stats.manaCurrent);
        playerStats.Add(PlayerStat.manaMax, stats.manaMax);
        playerStats.Add(PlayerStat.manaRecoverTime, stats.manaRecoverTime);
        playerStats.Add(PlayerStat.damage, stats.damage);
        playerStats.Add(PlayerStat.attackSpeed, stats.attackSpeed);
        playerStats.Add(PlayerStat.fireballDmg, stats.fireballDmg);
        playerStats.Add(PlayerStat.fireballSpeed, stats.fireballSpeed);
        playerStats.Add(PlayerStat.lightningDmg, stats.lightningDmg);
        playerStats.Add(PlayerStat.lightningSpeed, stats.lightningSpeed);
        playerStats.Add(PlayerStat.grabDmg, stats.grabDmg);
        playerStats.Add(PlayerStat.grabSpeed, stats.grabSpeed);
        playerStats.Add(PlayerStat.laserDmg, stats.laserDmg);
        playerStats.Add(PlayerStat.laserSpeed, stats.laserSpeed);
        playerStats.Add(PlayerStat.grenadeDmg, stats.grenadeDmg);
        playerStats.Add(PlayerStat.grenadeSpeed,stats.grenadeSpeed);
        playerStats.Add(PlayerStat.jumpHeight, stats.jumpHeight);
        playerStats.Add(PlayerStat.moveSpeed, stats.moveSpeed);
        playerStats.Add(PlayerStat.dashPower, stats.dashPower);

        player = gameObject;   
    }

    void UpdateStat(PlayerStat statToUpgrd, float amount)
    {
        playerStats[statToUpgrd] += amount;

        var result = statToUpgrd switch
        {
            hpCurrent => stats.hpCurrent = playerStats[PlayerStat.hpCurrent];

        }

        switch (statToUpgrd)
        {
            case < statToUpgrd.hp:
                Console.WriteLine($"Measured value is {measurement}; too low.");
                break;
        }
        stats.hpCurrent = playerStats[PlayerStat.hpCurrent];
        stats.hpMax = playerStats[PlayerStat.hpMax];
        stats.hpRecoverTime = playerStats[PlayerStat.hpRecoverTime];
        stats.manaCurrent = playerStats[PlayerStat.manaCurrent];
        stats.manaMax = playerStats[PlayerStat.manaMax];
        stats.manaRecoverTime = playerStats[PlayerStat.manaRecoverTime];
        stats.damage = playerStats[PlayerStat.damage];
        stats.attackSpeed = playerStats[PlayerStat.attackSpeed];
        stats.fireballDmg = playerStats[PlayerStat.fireballDmg];
        stats.fireballSpeed = playerStats[PlayerStat.fireballSpeed];
        stats.lightningDmg = playerStats[PlayerStat.lightningDmg];
        stats.lightningSpeed = playerStats[PlayerStat.lightningSpeed];
        stats.grabDmg = playerStats[PlayerStat.grabDmg];
        stats.grabSpeed = playerStats[PlayerStat.grabSpeed];
        stats.laserDmg = playerStats[PlayerStat.laserDmg];
        stats.laserSpeed = playerStats[PlayerStat.laserSpeed];
        stats.grenadeDmg = playerStats[PlayerStat.grenadeDmg];
        stats.grenadeSpeed = playerStats[PlayerStat.grenadeSpeed];
        stats.jumpHeight = playerStats[PlayerStat.jumpHeight];
        stats.jumpHeight = playerStats[PlayerStat.jumpHeight];
        stats.moveSpeed = playerStats[PlayerStat.moveSpeed];
        stats.dashPower = playerStats[PlayerStat.dashPower];
    }
    /*



    public float moveSpeed = 5;
    public float dashPower = 10;
     */





}

public enum PlayerStat
{
    hpMax, hpCurrent, hpRecoverTime,
    manaMax, manaCurrent, manaRecoverTime,
    damage, attackSpeed,
    fireballDmg, fireballSpeed,
    lightningDmg, lightningSpeed,
    grabDmg, grabSpeed,
    laserDmg, laserSpeed,
    grenadeDmg, grenadeSpeed,
    jumpHeight, moveSpeed,
    dashPower
}

/*
 *  hp = stats[PlayerStat.hpMax];
        stamina = player.GetComponent<StaminaControl>().GetMaxAmount();
        attackDamage = player.GetComponentInChildren<MeleeWeapon>().GetDamage();
        attackSpeed = player.GetComponentInChildren<MeleeWeapon>().GetAs();
        movementSpeed = player.GetComponent<MovementFin>().GetMovement();
        jumpPower = player.GetComponent<MovementFin>().GetJump();
        dashPower = player.GetComponent<MovementFin>().GetDash();
        gold = player.GetComponent<MoneyManager>().GetMoney();


 *     public float hp { get; private set; }
    public float stamina { get; private set; }
    public float attackDamage { get; private set; }
    public float attackSpeed { get; private set; }
    public float movementSpeed { get; private set; }
    public float jumpPower { get; private set; }
    public float dashPower { get; private set; }
    public float gold { get; private set; }



 // mozna dawac tez ujemne wartosci
    public void ChangeMaxHp(float amount)
    {
        hp =  player.GetComponent<HealthController>().AddMaxAmount(amount);
        updateGUI?.Invoke();
    }

    public void ChangeCurrentHp(float amount)
    {
        player.GetComponent<HealthController>().AddAmount(amount);
    }


    public void ChangeMaxStamina(float amount)
    {
        stamina = player.GetComponent<StaminaControl>().AddMaxAmount(amount);
        updateGUI?.Invoke();
    }

    public void ChangeCurrentStamina(float amount)
    {
        player.GetComponent<StaminaControl>().AddAmount(amount);
    }


    public void ChangeDamage(float amount)
    {
        attackDamage = player.GetComponentInChildren<MeleeWeapon>().ChangeDamage(amount);
        updateGUI?.Invoke();
    }

    public void ChangeAttackSpeed(float speed)
    {
        attackSpeed = player.GetComponentInChildren<MeleeWeapon>().ChangeAttackSpeed(speed);
        updateGUI?.Invoke();
    }

    public void ChangePlayerSpeed(float speed)
    {
        movementSpeed = player.GetComponent<MovementFin>().changeSpeed(speed);
        updateGUI?.Invoke();
    }

    public void ChangePlayerJump(float jump)
    {
        jumpPower = player.GetComponent<MovementFin>().ChangeJump(jump);
        updateGUI?.Invoke();
    }

    public void ChangePlayerDash(float dash)
    {
        dashPower = player.GetComponent<MovementFin>().ChangeDash(dash);
        updateGUI?.Invoke();
    }
    public void ChangeGoldValue(int money)
    {
        player.GetComponent<MoneyManager>().AddMoney(money);
        gold = player.GetComponent<MoneyManager>().GetMoney();
        updateGUI?.Invoke();
    }
 */