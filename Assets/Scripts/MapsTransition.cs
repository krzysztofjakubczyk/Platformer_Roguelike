using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] GameObject _doors;
    MoneyManager moneyManager;
    int howMoneyFromEnemy;
    [SerializeField]int howManyEnemies;
    private void Start()
    { 
        Entity.OnEnemyDeath += killAnEnemy;
        howManyEnemies = (GameObject.FindGameObjectsWithTag("Enemy").Length) / 2;
        moneyManager = FindAnyObjectByType<MoneyManager>();
    }
    void killAnEnemy()
    {
        howManyEnemies--;  
        Debug.Log(howManyEnemies);
        if (howManyEnemies == 0)
        {
            _doors.SetActive(false);
            moneyManager.AddMoney(howMoneyFromEnemy);
        }
    }
}
