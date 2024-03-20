using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDownTriangle : MonoBehaviour
{
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 20);
        Debug.DrawRay(transform.position, Vector2.down * 20, Color.green);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.name == "Player" && rb.tag == "RotatedTraingleObstacle")
            {
                rb.gravityScale = 1f;
            }
            else continue;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }
    }   
}
