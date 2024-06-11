using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwMove : MonoBehaviour
{
    public Vector2 dir;
    [SerializeField] float speed;

    public GameObject player;

    Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke(nameof(DestroyObject), 5);
    }

    void Update()
    {
        rb.velocity = dir.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            player.GetComponent<MovementFin>().DamageOnCollision(collision);

    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
