using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ifboxtriggered : MonoBehaviour
{

    public bool isTriggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isTriggered = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTriggered = false;
    }
}
