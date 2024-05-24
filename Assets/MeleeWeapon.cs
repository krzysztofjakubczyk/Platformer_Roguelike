using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float stunDamage;
    [SerializeField] float attackRate;
    [SerializeField] float attackDelay;

    bool canAttack = true;

    bool upPressed;

    GameObject player;
    Animator animator;
    Animator animatorSword;

    AttackDetails attackDetails = new AttackDetails();

    void Start()
    {
        player = transform.parent.gameObject;
        animator = player.GetComponent<Animator>();

        animatorSword = GetComponent<Animator>();

        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }


    void Update()
    {
        if (!canAttack)
            return;


        // TO DO:       wrzucic to do funkcji
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            upPressed = true;
        else
            upPressed = false;

        if (Input.GetKeyUp(KeyCode.C))
        {
            if (player.GetComponent<SpriteRenderer>().flipX)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                GetComponent<SpriteRenderer>().flipY = false;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                if (upPressed)
                {
                    GetComponent<SpriteRenderer>().flipX = false;

                    GetComponent<SpriteRenderer>().flipY = true;
                    transform.localPosition = new Vector2(1f, 3);
                    transform.rotation = Quaternion.Euler(0, 0, -90);
                    upPressed = false;
                }
                else
                {
                    transform.localPosition = new Vector2(1.5f, 1);
                }
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
                GetComponent<SpriteRenderer>().flipY = false;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                if (upPressed)
                {
                    transform.localPosition = new Vector2(-1.5f, 2);

                    GetComponent<SpriteRenderer>().flipY = false;
                    transform.localPosition = new Vector2(-1f, 3);
                    transform.rotation = Quaternion.Euler(0, 0, -90);
                    upPressed = false;
                }
                else
                {
                    transform.localPosition = new Vector2(-1.5f, 1);
                }
            }

            
                GetComponent<SpriteRenderer>().enabled = true;
                animator.SetTrigger("Attacking");
                animatorSword.SetTrigger("AttackingRn");
            

            Invoke(nameof(OnWeapon), attackDelay);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Enemy" || !canAttack)
        {
            return;
        }

        canAttack = false;
        Invoke(nameof(allowAttack), attackRate);
        
        attackDetails.position = transform.position;
        attackDetails.damageAmount = damage;
        attackDetails.stunDamageAmount = stunDamage;

        collision.transform.parent.GetComponent<Entity>().DamageGet(attackDetails);

    }

    void allowAttack()
    {
        canAttack = true;
    }

    void OnWeapon()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        Invoke(nameof(OffWeapon), 0.1f);
    }

    void OffWeapon()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void ChangeDamage(float amount)
    {
        damage += amount;
    }

    public void ChangeAttackSpeed(float amount)
    {
        attackRate += amount;
    }

}