using UnityEngine;

public class HealthController : StatConroller
{
    Animator animator;
    [SerializeField] SceneController sceneController;
    PlayerStatsFin playerStats;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy") || collision.transform.CompareTag("enemyBullet"))
        {
            SubAmount(5);
            animator.SetTrigger("Hurting");

        }
    }

    public void Start()
    {
        StartCoroutine(RecoverNew());
        if (GetComponent<PlayerStatsManager>() != null)
        {
            playerStats = GetComponent<PlayerStatsManager>().playerStats;
            UpdateAllStats();
        }


    }

    public void DamagePlayer(float amount)
    {
        SubAmount(amount);

        animator.SetTrigger("Hurting");
    }

    public void UpdateAllStats()
    {
        foreach (var stat in playerStats.stats)
        {
            switch (stat.statName)
            {
                case PlayerStatEnum.hpMax:
                    maxAmount = stat.value;
                    break;
                case PlayerStatEnum.hpCurrent:
                    currentAmount = stat.value;
                    break;
                case PlayerStatEnum.hpRecoverTime:
                    recoverTime = stat.value;
                    break;
            }
        }
    }
    public void OnDeath()
    {
        sceneController.onPlayersDeath();
    }
}
