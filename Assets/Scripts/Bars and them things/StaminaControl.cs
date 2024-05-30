

public class StaminaControl : StatConroller
{
    PlayerStatsFin playerStats;

    public void Start()
    {
        StartCoroutine(RecoverNew());
        playerStats = GetComponent<PlayerStatsManager>().playerStats;
        UpdateAllStats();

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
