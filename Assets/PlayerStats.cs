using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
//using static Player_Anim_State;

public class PlayerStatsManager : MonoBehaviour
{
    public PlayerStatsFin playerStats;

    private void Start()
    {
        foreach (var stat in playerStats.stats)
        {
            Debug.Log($"{stat.statName}: {stat.value}");
        }
    }

    public void UpdateStat(PlayerStatEnum statName, float newValue)
    {
        foreach (var stat in playerStats.stats)
        {
            if (stat.statName == statName)
            {
                stat.value = newValue;
                Debug.Log($"{stat.statName} updated to: {newValue}");
                return;
            }
        }

        Debug.LogWarning($"Stat {statName} not found!");
    }
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