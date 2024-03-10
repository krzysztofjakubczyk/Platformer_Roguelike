using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kula : MonoBehaviour
{
    [SerializeField]
    private float force;
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.right * force);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "dontDestroy") return;
        collision.gameObject.SetActive(false);
    }

}
