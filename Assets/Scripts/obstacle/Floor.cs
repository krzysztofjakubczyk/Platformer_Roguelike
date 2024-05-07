using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] MovementFin playerMovement;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerMovement.changeSpeed(2);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerMovement.changeSpeed(8);
    }
}
