using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpike : MonoBehaviour
{
    [SerializeField] GameObject pointToSpawn;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.position = pointToSpawn.transform.position;
        }
    }

}
