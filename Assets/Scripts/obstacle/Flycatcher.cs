using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flycatcher : MonoBehaviour
{
    [SerializeField] List<GameObject> Walls;
    private bool isCollision = false;

    //TODO do³¹czyæ œcianê do niszczenia
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isCollision == false)
        {
            isCollision = true;
            foreach(GameObject wall in Walls)
            {
                wall.SetActive(true);
            }
        }
        else if (isCollision)
        {
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
