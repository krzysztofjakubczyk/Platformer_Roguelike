using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player_Anim_State;

public class SpellManager : MonoBehaviour
{
    //[SerializeField] GameObject fireBall;
    //[SerializeField] GameObject lightning;
    //[SerializeField] GameObject grabSpell;
    //[SerializeField] GameObject laserSpell;
    //[SerializeField] GameObject grenadeSpell;

    [SerializeField] GameObject activeSpell;
    [SerializeField] float yOffset;
    StaminaControl StaminaControl;

    PlayerStatsFin playerStats;
    float fireBallDmg;
    float lightningDmg;
    float grabSpellDmg;
    float laserSpellDmg;
    float grenadeSpellDmg;

    float cost;
    Vector2 throwDir;
    bool canCast;
    [SerializeField]
    public GameObject[] spells;


    void Start()
    {
        
        StaminaControl = GetComponent<StaminaControl>();

        canCast = true;

        playerStats = GetComponent<PlayerStatsManager>().playerStats;
        UpdateAllStats();

    }


    void Update()
    {
        // TO DO:       zmienna zamiast keycode

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            throwDir = transform.up;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            throwDir = transform.right;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            throwDir = -transform.right;
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            UpdateAllStats();
        }


        if (Input.GetKeyDown(KeyCode.X) && activeSpell != null && canCast)
        {
            canCast = false;
            float chargeTime = activeSpell.GetComponent<Spell>().reachargeTime;
            Invoke(nameof(CanCastAgain), chargeTime);

            cost = activeSpell.GetComponent<Spell>().cost;

            if (StaminaControl.GetCurrentAmount() > cost)
            {
                StaminaControl.SubAmount(cost);
                    
                activeSpell.GetComponent<Spell>().castDirection = throwDir;
                activeSpell.GetComponent<Spell>().player = gameObject;
                activeSpell.GetComponent<Spell>().rb = activeSpell.GetComponent<Rigidbody2D>();
                

                Vector2 startPos = new Vector2(transform.position.x, transform.position.y + yOffset);
                GameObject newSpell = Instantiate(activeSpell, startPos,Quaternion.identity);

                switch (activeSpell.tag)
                {
                    case "fireball":
                        newSpell.GetComponent<Spell>().damage = fireBallDmg;
                        break;
                    case "grabSpell":
                        newSpell.GetComponent<Spell>().damage = grabSpellDmg;
                        break;
                    case "greandeSpell":
                        newSpell.GetComponent<Spell>().damage = grenadeSpellDmg;
                        break;
                    case "laserSpell":
                        newSpell.GetComponent<Spell>().damage = laserSpellDmg;
                        break;
                    case "lightningspell":
                        newSpell.GetComponent<Spell>().damage = lightningDmg;
                        break;
                }

                if (throwDir.y == transform.up.y)
                    newSpell.transform.localRotation = Quaternion.Euler(0, 0, 90);
                else if (throwDir.x == -transform.right.x)
                    newSpell.GetComponent<SpriteRenderer>().flipX = true;
                print(newSpell.tag);

            }
        }
    }

    public void UpdateAllStats()
    {
        foreach (var stat in playerStats.stats)
        {
            switch (stat.statName)
            {
                case PlayerStatEnum.fireballDmg:
                    fireBallDmg = stat.value;
                    break;
                case PlayerStatEnum.lightningDmg:
                    lightningDmg = stat.value;
                    break;
                case PlayerStatEnum.grabDmg:
                    grabSpellDmg = stat.value;
                    break;
                case PlayerStatEnum.laserDmg:
                    laserSpellDmg = stat.value;
                    break;
                case PlayerStatEnum.grenadeDmg:
                    grenadeSpellDmg = stat.value;
                    break;
            }
        }
    }

    public void setSpell(GameObject spell)
    {
          activeSpell = spell;
    }

    void CanCastAgain()
    {
        canCast = true;
    }
}
