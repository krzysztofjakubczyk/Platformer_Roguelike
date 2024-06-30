using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addHpMana_Stats : MonoBehaviour
{
    [SerializeField] PlayerStatsManager stats;
    [SerializeField] GameObject player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stats = player.GetComponent<PlayerStatsManager>();
        foreach (var stat in stats.playerStats.stats)
        {
            if (stat.statName == PlayerStatEnum.hpCurrent)
                stat.value = player.GetComponent<HealthController>().GetCurrentAmount();
            else if (stat.statName == PlayerStatEnum.manaCurrent)
                stat.value = player.GetComponent<StaminaControl>().GetCurrentAmount();
        }
    }
}
