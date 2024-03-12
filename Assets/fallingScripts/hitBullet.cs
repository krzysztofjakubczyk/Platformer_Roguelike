using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitBullet : MonoBehaviour
{
    [SerializeField] private float force;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

            // Zatrzymaj ruch gracza
            rb.velocity = Vector2.zero;

            // Odbij gracza w przeciwnym kierunku z mniejsz¹ prêdkoœci¹
            rb.velocity = new Vector2(-rb.velocity.x, -rb.velocity.y) * 0.5f;
            rb.AddForce(Vector2.right * 100000);

            // Uruchom efekty wizualne
            // np. zmieñ kolor gracza na czerwony
            collision.GetComponent<SpriteRenderer>().color = Color.red;
            //collision.GetComponent<Rigidbody2D>().AddForce(-collision.transform.rotation.eulerAngles);
        }
    }
}