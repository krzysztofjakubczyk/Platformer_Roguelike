using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformWalking : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] LayerMask platformLayer;
    bool timeOff;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        timeOff = false;
    }
    private void Update()
    {
        Vector2 rayOrigin = new Vector2(transform.position.x, transform.position.y);
        if(!Physics2D.Raycast(rayOrigin, -transform.up, 1, platformLayer) && !timeOff) //dobry kod danielo 
        {
            timeOff = true;

            Invoke(nameof(RotateEnemy), 0.2f);

            Invoke(nameof(noTimeOff), 0.8f);
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnDrawGizmos()
    {
        Vector2 rayOrigin = new Vector2(transform.position.x, transform.position.y);
        Gizmos.DrawRay(rayOrigin, -transform.up);
    }

    void noTimeOff()
    {
        timeOff = false;
    }

    void RotateEnemy()
    {
        transform.Rotate(0, 0, -90);
    }
}
