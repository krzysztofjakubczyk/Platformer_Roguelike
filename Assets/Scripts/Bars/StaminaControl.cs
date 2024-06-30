

using System.Collections;
using UnityEngine;

public class StaminaControl : StatConroller
{
    PlayerStatsFin playerStats;

    public void Start()
    {
        StartCoroutine(loadWithDelay());
        playerStats = GetComponent<PlayerStatsManager>().playerStats;
        UpdateAllStats();

    }
    private IEnumerator loadWithDelay()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(RecoverNew());   
    }
    public void UpdateAllStats()
    {
        foreach (var stat in playerStats.stats)
        {
            switch (stat.statName)
            {
                case PlayerStatEnum.manaMax:
                    maxAmount = stat.value;
                    break;
                case PlayerStatEnum.manaCurrent:
                    currentAmount = stat.value;
                    break;
                case PlayerStatEnum.manaRecoverTime:
                    recoverTime = stat.value;
                    break;
            }
        }
    }
}
