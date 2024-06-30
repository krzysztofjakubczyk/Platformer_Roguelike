using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class HealthController : StatConroller
{
    Animator animator;
    [SerializeField] SceneController sceneController;
    PlayerStatsFin playerStats;

    [SerializeField]GameObject vignette;
    byte vinetteVisible;
    [SerializeField] GameObject dontDestroyOnLoad;
    [SerializeField] AudioClip gettingHit;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy") || collision.transform.CompareTag("enemyBullet"))
        {
            //SubAmount(5);
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
        StartCoroutine(HideBlood());
        animator.SetTrigger("Hurting");
        GetComponent<AudioSource>().PlayOneShot(gettingHit);
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
        GameObject.Destroy(dontDestroyOnLoad);
    }

    IEnumerator HideBlood()
    {
        vignette.SetActive(true);
        vinetteVisible = 255;

        while (vinetteVisible > 20)
        {
            vignette.GetComponent<UnityEngine.UI.Image>().color = new Color32(255, 0, 0, vinetteVisible);
            vinetteVisible -= 15;
            yield return new WaitForSeconds(0.01f);
        }

        vignette.SetActive(false);
    }
}
