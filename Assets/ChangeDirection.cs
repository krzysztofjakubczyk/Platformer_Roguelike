using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDirection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D colider)
    {
        if (colider.tag == "Enemy")
        {
            colider.transform.Rotate(0f, 180f, 0f);
        }
    }
}
