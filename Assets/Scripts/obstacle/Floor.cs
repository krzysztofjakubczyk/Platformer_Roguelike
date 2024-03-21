using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] mvmnt playerMovement;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerMovement.ChangePlayerSpeed(2);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerMovement.ChangePlayerSpeed(8);
    }
}
