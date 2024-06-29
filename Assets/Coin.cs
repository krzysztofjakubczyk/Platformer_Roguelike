using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Coin : MonoBehaviour
{
    public delegate void AddMoneyDelegate(int amount);
    public static event AddMoneyDelegate addMoney; 
    Animator anim;
    [SerializeField]
    private float force;
    [Range(1,50)]
    [SerializeField] 
    private int amount;
    [Range(1, 10)]
    [SerializeField]
    private int amountBounce;
    private int counterBounce;
    [SerializeField]
    private GameObject pickUpPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Bounce();
        Invoke(nameof(ReadyToCollect), 3); 
    }
    void ReadyToCollect()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        anim.enabled = true;
        //GetComponent<CircleCollider2D>().isTrigger = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            //tu bêdzie dodawanie monet dla gracza
            addMoney?.Invoke(amount);
            Instantiate(pickUpPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            if(counterBounce<= amountBounce)
            {
                Bounce();
                counterBounce++;
            }
            else
            {
                counterBounce = 0;
            }
            
        }
    }
    private void Bounce()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
        float x = UnityEngine.Random.Range(-1f, 1f);
        float y = UnityEngine.Random.Range(0.5f, 1f);
        Vector2 startVector = new Vector2(x, y);
        GetComponent<Rigidbody2D>().AddForce(startVector * force, ForceMode2D.Impulse);
    }
}
