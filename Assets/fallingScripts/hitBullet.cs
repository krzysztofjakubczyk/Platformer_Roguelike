using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitBullet : MonoBehaviour
{
    [SerializeField] private float force;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Rigidbody2D>().AddForce(Vector2.up * force);
        //collision.GetComponent<Rigidbody2D>().AddForce(-collision.transform.rotation.eulerAngles);
    }
}
