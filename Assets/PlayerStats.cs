using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    GameObject player;


    void Start()
    {
        player = gameObject;
    }

    // mozna dawac tez ujemne wartosci
    public void ChangeMaxHp(float amount)
    {
        player.GetComponent<HealthController>().AddMaxAmount(amount);
    }

    public void ChangeCurrentHp(float amount)
    {
        player.GetComponent<HealthController>().AddAmount(amount);
    }


    public void ChangeMaxStamina(float amount)
    {
        player.GetComponent<StaminaControl>().AddMaxAmount(amount);
    }

    public void ChangeCurrentStamina(float amount)
    {
        player.GetComponent<StaminaControl>().AddAmount(amount);
    }


    public void ChangeDamage(float amount)
    {
        player.GetComponent<MeleeWeapon>().ChangeDamage(amount);
    }

    public void ChangeAttackSpeed(float speed)
    {
        player.GetComponent<MeleeWeapon>().ChangeAttackSpeed(speed);
    }

    public void ChangePlayerSpeed(float speed)
    {
        player.GetComponent<MovementFin>().changeSpeed(speed);
    }

    public void ChangePlayerJump(float jump)
    {
        player.GetComponent<MovementFin>().ChangeJump(jump);
    }

    public void ChangePlayerDash(float dash)
    {
        player.GetComponent<MovementFin>().ChangeDash(dash);
    }
}
