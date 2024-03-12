using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class enemy1_edges : MonoBehaviour
{
    [SerializeField] float speed;
    int horizontal;
    [SerializeField] bool startWPrawo;
    BoxCollider2D coll;
    Rigidbody2D rb;
    [SerializeField]LayerMask platformLayer;

    bool zeroVel;

    void Start()
    {
        zeroVel = false;
        coll = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        if (startWPrawo)
            horizontal = 1;
        else
            horizontal = -1;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 borderL = new Vector2(coll.bounds.center.x - 0.5f, coll.bounds.center.y - 0.3f);
        Vector2 borderR = new Vector2(coll.bounds.center.x + 0.5f, coll.bounds.center.y - 0.3f);

        // zeroVel - bo musi miec czas na oddalenie sie od krawedzi (zeby tylko raz ustawic velocity na 0) - do zmiany
        if (!Physics2D.BoxCast(borderL, coll.bounds.size, 0f, Vector2.left, .1f, platformLayer) && !zeroVel)
        {
            zeroVel = true;
            rb.velocity = Vector2.zero;
            horizontal = 1;
            Invoke(nameof(zeroBack), 0.5f);
        }

        if(!Physics2D.BoxCast(borderR, coll.bounds.size, 0f, Vector2.left, .1f, platformLayer) && !zeroVel)
        {
            zeroVel = true;
            rb.velocity = Vector2.zero;
            horizontal = -1;
            
            Invoke(nameof(zeroBack), 0.5f);
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector2(horizontal * speed, 0));
    }


    void zeroBack()
    {
        zeroVel = false;
    }
}
