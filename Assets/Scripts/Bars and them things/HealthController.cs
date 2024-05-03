using UnityEngine;

public class HealthController : StatConroller
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy") || collision.transform.CompareTag("enemyBullet"))
        {
            SubAmount(1);
            animator.SetTrigger("Hurting");

        }
    }

    public void DamagePlayer(float amount)
    {
        SubAmount(amount);

        animator.SetTrigger("Hurting");
    }
}
