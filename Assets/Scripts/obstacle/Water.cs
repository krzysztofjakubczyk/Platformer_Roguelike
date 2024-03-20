using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] healthController health;
    private void OnCollisionEnter2D(Collision2D collision)
    {
    if(collision.transform.name == "Player")
        {
            StartCoroutine(ApplyDamageOverTime());
        }      
    }
    private IEnumerator ApplyDamageOverTime()
    {
        // Pêtla nieskoñczona
        while (true)
        {
            // Odczekaj okreœlony czas
            yield return new WaitForSeconds(1);

            // Odjêcie ¿ycia gracza
            health.MinusHP(1);
        }   
    }
}
