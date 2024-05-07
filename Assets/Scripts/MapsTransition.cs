using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] GameObject _doors;
    [SerializeField]int howManyEnemies;
    private void Start()
    { 
        Entity.OnEnemyDeath += killAnEnemy;
        howManyEnemies = (GameObject.FindGameObjectsWithTag("Enemy").Length) / 2;
    }
    void killAnEnemy()
    {
        howManyEnemies--;  
        Debug.Log(howManyEnemies);
        if (howManyEnemies == 0)
        {
            _doors.SetActive(false);
        }
    }
}
