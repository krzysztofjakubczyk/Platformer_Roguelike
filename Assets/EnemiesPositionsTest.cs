using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPositionsTest : MonoBehaviour
{
    [SerializeField] GameObject enemie1;
    [SerializeField] GameObject enemie2;
    [SerializeField] GameObject enemie3;
    [SerializeField] GameObject enemie4;

    Vector2 pos1;
    Vector2 pos2;
    Vector2 pos3;
    Vector2 pos4;

    void Start()
    {
        pos1 = enemie1.transform.position;
        pos2 = enemie2.transform.position;
        pos3 = enemie3.transform.position;
        pos4 = enemie4.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            enemie1.transform.position = pos1;
            enemie2.transform.position = pos2;
            enemie3.transform.position = pos3;
            enemie4.transform.position = pos4;

            enemie1.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            enemie2.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            enemie3.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            enemie4.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
