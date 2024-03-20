using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flycatcher : MonoBehaviour
{
    [SerializeField] List<GameObject> Walls;
    //TODO do³¹czyæ œcianê do niszczenia
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach(GameObject wall in Walls)
            {
                wall.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
