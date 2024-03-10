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
        if (collision.transform.tag == "player") collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * speed);
    }

    private void FixedUpdate()
    {
        rb.velocity = vectorMove * speed;

    }

}
