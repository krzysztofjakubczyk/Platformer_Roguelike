using System;
using UnityEngine;
//using static Player_Anim_State;

public class PlayerStatsManager : MonoBehaviour
{
    public PlayerStatsFin playerStats;
    [SerializeField] PlayerStatsFin playerStatsDefault;
    GameObject player;
    GameObject sword;
    public Action updateGUI;

    private void Start()
    {
        player = gameObject;
        sword = player.transform.GetChild(0).gameObject;
        Invoke(nameof(ResetStats),0.1f);
    }

    public void UpdateStat(PlayerStatEnum statName, float newValue)
    {
        foreach (var stat in playerStats.stats)
        {
            if (stat.statName == statName)
            {
                stat.value += newValue;
                Debug.Log($"{stat.statName} updated to: {stat.value}");

                // update stats where they are used
                player.GetComponent<MovementFin>().UpdateAllStats();
                sword.GetComponent<MeleeWeapon>().UpdateAllStats();
                player.GetComponent<StaminaControl>().UpdateAllStats();
                player.GetComponent<HealthController>().UpdateAllStats();
                player.GetComponent<SpellManager>().UpdateAllStats();


                return;
            }
        }
        updateGUI?.Invoke();
        Debug.LogWarning($"Stat {statName} not found!");
    }

    public void ResetStats()
    {
        foreach (var stat in playerStats.stats)
        {
            foreach (var statD in playerStatsDefault.stats)
                if (stat.statName == statD.statName)
                {
                    Debug.Log($"{stat.statName} before: {stat.value}");
                    stat.value = statD.value;
                    Debug.Log($"{stat.statName} updated to: {stat.value}");
                    break;
                }
        }
        player.GetComponent<MovementFin>().UpdateAllStats();
        sword.GetComponent<MeleeWeapon>().UpdateAllStats();
        player.GetComponent<StaminaControl>().UpdateAllStats();
        player.GetComponent<HealthController>().UpdateAllStats();
        player.GetComponent<SpellManager>().UpdateAllStats();
        updateGUI?.Invoke();
    }
}