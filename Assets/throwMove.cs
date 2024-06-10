using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwMove : MonoBehaviour
{
    public Vector2 dir;
    [SerializeField] float speed;

    Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = dir.normalized * speed;
    }
}
