using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BirdPatrol : MonoBehaviour
{
    [SerializeField] private float speed;
    Vector2 vectorMove = Vector2.right;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "dontDestroy") vectorMove = -vectorMove;
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

            // Zatrzymaj ruch gracza
            rb.velocity = Vector2.zero;

            // Odbij gracza w przeciwnym kierunku z mniejsz¹ prêdkoœci¹
            rb.velocity = new Vector2(-rb.velocity.x, -rb.velocity.y) * 0.5f;
            rb.AddForce(Vector2.up * 1000);

            // Uruchom efekty wizualne
            // np. zmieñ kolor gracza na czerwony
            collision.GetComponent<SpriteRenderer>().color = Color.red;
            Debug.Log("trafi³em ptaka");
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = vectorMove * speed;

    }

}
