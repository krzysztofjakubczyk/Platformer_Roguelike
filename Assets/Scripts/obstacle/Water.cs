using System.Collections;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] healthController health;
    bool isPosioned = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name == "Player")
        {
            isPosioned = true;
            StartCoroutine(ApplyDamageOverTime());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // SprawdŸ, czy gracz wyszed³ z wody
        if (collision.transform.name == "Player")
        {
            isPosioned = false;
            // Zatrzymaj korutynê odjêcia ¿ycia w wodzie
            StopCoroutine(ApplyDamageOverTime());
        }
    }
    private IEnumerator ApplyDamageOverTime()
    {
        while (isPosioned == true) 
        {
            // Odjêcie ¿ycia gracza
            health.MinusHP(1);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
